using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    //public GameObject Rockdivided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            //Instantiate(Rockdivided, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        Debug.Log("La piedra se rompio en pedazos");
    }
}
