using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    Player_Movement _movement;
    Player_Inputs _inputs;
    Player_Collisions _collisions;
    Player_Attacks _attacks;
    Rigidbody _rb;
    LifeHandler _lifeHandler;
    [SerializeField] int _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _dashForce;
    [SerializeField] float _dashDuration;
    [SerializeField] float _dashCooldown;
 
    [SerializeField] float _glideDrag;
    [SerializeField] Transform checkpoint;
    [SerializeField] int _slimeForce;
    public int slimeDmg;
    public int zonesDmg;
    public int twisterDmg;
    public float twisterForce;
    
    
    
    Vector3 _initialPosition;

    public Transform testBossPoint;


    private void Awake()
    {
        _life = _maxLife;
        _rb = GetComponent<Rigidbody>();
        _lifeHandler = new LifeHandler();
        _inputs = new Player_Inputs(transform , _lifeHandler , this);
        _movement = new Player_Movement(_rb , _inputs , _speed, _jumpForce , _dashForce, _dashDuration,_dashCooldown , transform ,_glideDrag , _lifeHandler , _slimeForce , this);
        _collisions = new Player_Collisions(_movement , _rb, checkpoint, this , transform);
        _attacks = new Player_Attacks(transform , _inputs);


        #region Inputs

        _inputs.BlindKeys(KeyCode.Space, new JumpInputs(_movement));
        _inputs.BlindKeys(KeyCode.LeftShift, new DashInput(_movement));
        _inputs.BlindKeys(KeyCode.E, new GlideInput(_movement));
        _inputs.BlindKeys(KeyCode.Mouse0, new ShootInput(_attacks));
        _inputs.BlindKeysUp(KeyCode.E, new NotGlideInput(_movement));
       
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        _movement.ArtificialStart();
        //transform.position = _initialPosition;
        _inputs.ArtificialStart();
    }

    // Update is called once per frame
    void Update()
    {
        _inputs.ArtificialUpdate();


        CommandInputs keypressed = _inputs.Inputs();
        if (keypressed != null)
        {
            keypressed.Execute();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.position = testBossPoint.position;
        }
    }

    

    private void FixedUpdate()
    {
        _movement.ArtificialUpdate();
        
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

        CheckLife();
    }

    void CheckLife()
    {
        if (_life<=0)
            _lifeHandler.OnDead();
        

    }
}
