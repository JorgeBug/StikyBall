using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CamaraLookAtBall : MonoBehaviour
{
    public GameObject ball; //Este actua como una referencia 
    Vector3 lookAtOffset; //Para el desplazamiento sobre la pelota en la funcion de inicio


    // Start is called before the first frame update
    void Start()
    {
        //Con esto podemos configurar el vector3
        lookAtOffset = new Vector3(0, 1, 0); //(posición X, posición Y, posición Z)
    }

    // Update is called once per frame
    void Update ()
    {
        // mirar al balón
        transform.LookAt(ball.transform.position + lookAtOffset);
        //Tomar la referencia balón, obtener la posición
    }
}
