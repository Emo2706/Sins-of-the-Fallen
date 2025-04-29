using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBullet : MonoBehaviour
{
    public Bullet[] bullets;
    private int indiceBalaActual = 0;

    private void Update()
    {
        RevisarCambio();
    }

    void CambiarBalaActual()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        bullets[indiceBalaActual].gameObject.SetActive(true);
    }

    void RevisarCambio()
    {
        float ruedaMouse = Input.GetAxis("Mouse ScrollWheel");
        if (ruedaMouse > 0f)
        {
            SeleccionarArmaAnterior();
        }
        else if (ruedaMouse < 0f)
        {
            SeleccionarArmaSiguiente();
        }
    }

    void SeleccionarArmaAnterior()
    {
        if (indiceBalaActual == 0)
        {
            indiceBalaActual = bullets.Length - 1;
        }
        else
        {
            indiceBalaActual--;
        }
        CambiarBalaActual();
    }

    void SeleccionarArmaSiguiente()
    {
        if (indiceBalaActual >= (bullets.Length - 1))
        {
            indiceBalaActual = 0;
        }
        else
        {
            indiceBalaActual++;
        }
        CambiarBalaActual();
    }
}
