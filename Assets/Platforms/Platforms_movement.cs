using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms_movement : MonoBehaviour
{
    public GameObject platform;
    public Transform endPoint;
    public Transform startPoint;
    public int speed;
    Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _direction = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, _direction, speed * Time.deltaTime);

        if (platform.transform.position==endPoint.position)
        {
            _direction = startPoint.position;
        }

        if (platform.transform.position==startPoint.position)
        {
            _direction = endPoint.position;
        }
    }
}
