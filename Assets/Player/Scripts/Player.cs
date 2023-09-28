using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    Player_Movement _movement;
    Player_Inputs _inputs;
    Player_Collisions _collisions;
    Rigidbody _rb;
    [SerializeField] int _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _dashForce;
    [SerializeField] float _dashDuration;
    [SerializeField] float _dashCooldown;
 
    [SerializeField] float _glideDrag;
    [SerializeField] Transform checkpoint;
    
    Vector3 _initialPosition;



    private void Awake()
    {
        _life = _maxLife;
        _rb = GetComponent<Rigidbody>();
        _inputs = new Player_Inputs(transform);
        _movement = new Player_Movement(_rb , _inputs , _speed, _jumpForce , _dashForce, _dashDuration,_dashCooldown , transform ,_glideDrag);
        _collisions = new Player_Collisions(_movement , _rb, checkpoint, this);

        _inputs.BlindKeys(KeyCode.Space, new JumpInputs(_movement));
        _inputs.BlindKeys(KeyCode.LeftShift, new DashInput(_movement));
        _inputs.BlindKeys(KeyCode.E, new GlideInput(_movement));
    }
    // Start is called before the first frame update
    void Start()
    {
        _movement.ArtificialStart();
        //transform.position = _initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _inputs.ArtificialUpdate();


        CommandInputs keypressed = _inputs.Inputs();
        if (keypressed!= null)
        {
            keypressed.Execute();
        }
    }

    private void FixedUpdate()
    {
        _movement.ArtificialUpdate();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisions.ArtificialOnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        _collisions.ArtificialOnCollisionExit(collision);
    }
}
