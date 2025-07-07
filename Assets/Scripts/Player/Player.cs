using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Player : Entity
{
    Player_Movement _movement;
    Player_Inputs _inputs;
    Player_Collisions _collisions;
    Player_Attacks _attacks;
    Player_UI _ui;
    SliderUI _sliderUI;
    PlayerView _view;
    public ScriptableRendererFeature dashWind;
    [SerializeField] Rigidbody _rb;
    LifeHandler _lifeHandler;
    [SerializeField] Transform checkpoint;
    public GameObject dashParticles;

    [Header("Cooldowns")]
    [SerializeField] float _dashDuration;
    [SerializeField] float _dashCooldown;
    public float shootCooldown;
    public float[] phasesCooldowns;


    [Header("Vars")]
    [SerializeField] float _glideDrag;
    [SerializeField] float _glideForce;
    [SerializeField] int _slimeForce;
    [SerializeField] int _amountPowerUpBullets;
    [SerializeField] int _multiplierDmg;
    [SerializeField] float _shakeDuration;
    [SerializeField] float _shakeMagnitude;
    public float smoothValue;
    public int[] phaseDamages;
    public int slimeDmg;
    public int zonesDmg;
    public int twisterDmg;
    public float twisterForce;
    public int circleDmg;
    public int bulletsDmg;
    public int enemyDmg;
    public int lavaDmg;
    public int lavaCooldown;
    [SerializeField] int _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _dashForce;
    public int phase1Dmg;
    public int phase2Dmg;
    public List<GameObject> FireUI;
    public List<GameObject> IceUI;
    public Animator anim, armAnim;

    public Image imgFire;

    public Material matFire;

    public GameObject[] fireMarkers;
    public GameObject[] iceMarkers;

    [SerializeField] Image _hpBar;
    public int haiserForce;
    public Slider sliderFireBall;
    public Slider sliderIceBall;

    public TMP_Text fireTxt;
    public TMP_Text iceTxt;

    [SerializeField] FirstPersonCamera _cam;
    [SerializeField] Transform _headTransform;
    [SerializeField] Transform _pivotShoot;
    public Transform pivotParticles;


    [Range(1f, 200f)] public float sensitivity = 200f;

    public float movRange;

    Vector3 _initialPosition;

    public Transform testBossPoint;

    public LayerMask movMask = 1 << 15;


    private void Awake()
    {
        _cam = Camera.main.GetComponent<FirstPersonCamera>();
        _cam.head = _headTransform;

        life = _maxLife;

        _lifeHandler = new LifeHandler();
        _inputs = new Player_Inputs(transform, _lifeHandler, this);
        _movement = new Player_Movement(_rb, _inputs, _speed, _jumpForce, _dashForce, _dashDuration, _dashCooldown, transform, _glideDrag, _glideForce, _lifeHandler, _slimeForce, this, _cam);
        _collisions = new Player_Collisions(_movement, _rb, checkpoint, this, transform, _lifeHandler);
        _sliderUI = new SliderUI(this);
        _ui = new Player_UI(_hpBar, this, _sliderUI);
        _view = new PlayerView(this);
        _attacks = new Player_Attacks(_amountPowerUpBullets, _multiplierDmg, _pivotShoot, this, _sliderUI, _ui, _view);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        #region Inputs

        _inputs.BlindKeys(KeyCode.Space, new JumpInputs(_movement));
        _inputs.BlindKeys(KeyCode.LeftShift, new DashInput(_movement));
        _inputs.BlindKeysHold(KeyCode.Space, new GlideInput(_movement)); 
        _inputs.BlindKeys(KeyCode.Q, new ChangeBulletInput(_attacks));
        _inputs.BlindKeysUp(KeyCode.Space, new NotGlideInput(_movement));
        _inputs.BlindKeysUp(KeyCode.Mouse0, new ChargeUpInput(_attacks));
        _inputs.BlindKeysHold(KeyCode.Mouse0, new ChargeInput(_attacks));
       
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        _movement.ArtificialStart();
        //transform.position = _initialPosition;
        _inputs.ArtificialStart();
        _inputs.CompleteData(_movement);

        AudioManager.instance.Play(AudioManager.Sounds.Ambience);

        /*CheckPointManager.instance.SetPlayer(this);*/
        //transform.position = CheckPointManager.instance.CheckPoint();

        _sliderUI.Start();
    }

    // Update is called once per frame
    void Update()
    {
        _inputs.ArtificialUpdate();
        _inputs.UpdateInputs();
        _ui.Update();
        _movement.Update();
        _sliderUI.Update();

        CommandInputs keypressed = _inputs.Inputs();
        if (keypressed != null)
        {
            keypressed.Execute();
        }

        foreach (CommandInputs input in _inputs.HoldInputs)
        {
            input.Execute();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.position = testBossPoint.position;
            
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            LevelManager.instance.StartLevel(2);
        }
    }

    public void PowerUpBullet()
    {
        _attacks.PowerUpBullet();
    }

    private void FixedUpdate()
    {
        _movement.FixedUpdate();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisions.OnCollisionEnter(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisions.OnTriggerEnter(other);
    }

    private void OnCollisionExit(Collision collision)
    {
        _collisions.OnCollisionExit(collision);
    }



    public override void TakeDmg(int dmg)
    {
        base.TakeDmg(dmg);

        AudioManager.instance.PlayRandom(new int[] { AudioManager.Sounds.Hurt1, AudioManager.Sounds.Hurt2 });

        CheckLife();

    }

    void CheckLife()
    {
        if (life<=0)
            _lifeHandler.OnDead();
        

    }

    public void ShakeCamera()
    {
        StartCoroutine(_cam.ShakeCourotine(_shakeDuration, _shakeMagnitude));
    }
}
