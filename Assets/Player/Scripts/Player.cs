using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Player_Movement _movement;
    Player_Inputs _inputs;
    Player_Collisions _collisions;
    Rigidbody _rb;
    [SerializeField] int _speed;
    [SerializeField] float _jumpForce;
    Vector3 _initialPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputs = new Player_Inputs();
        _movement = new Player_Movement(_rb , _inputs , _speed, _jumpForce);
        _collisions = new Player_Collisions(_movement);

        _inputs.BlindKeys(KeyCode.Space, new JumpInputs());
    }
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = _initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _inputs.ArtificialUpdate();
        _movement.ArtificialUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisions.ArtificialOnCollisionEnter(collision);
    }
}
