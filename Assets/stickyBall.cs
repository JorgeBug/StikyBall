using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stickyBall : MonoBehaviour
{
    //Grado para nuestro �ngulo de orientaci�n
    public float facingAngle = 0;
    float x = 0;
    float z = 0;
    Vector2 unitV2;

    public GameObject cameraReference;
    float distanceToCamera = 5;

    float size = 1;

    public GameObject category1;
    bool category1Unlocked = false;
    public GameObject category2;
    bool category2Unlocked = false;
    public GameObject category3;
    bool category3Unlocked = false;

    public GameObject sizeUI;

    public AudioClip pickupSound;




    // Update is called once per frame
    void Update()
    {


        //Control de usuario o User Conrtrols
        x = Input.GetAxis("Horizontal") * Time.deltaTime * -100;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 500;

        //Angulo de orientaci�n o Facing Angle
        facingAngle += x;
        unitV2 = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));
    }

    private void FixedUpdate()
    {
        //Apply forced Behind
        this.transform.GetComponent<Rigidbody>().AddForce(new Vector3(unitV2.x, 0, unitV2.y) * z * 3);

        //Establecer posici�n de la camara o set camera Position Behind the ball Based On Rotation 
        cameraReference.transform.position = new Vector3(-unitV2.x * distanceToCamera, distanceToCamera, -unitV2.y * distanceToCamera) + this.transform.position;

        unlockPickupCategories();
    }

    void unlockPickupCategories()
    {
        if (category1Unlocked == false)
        {
            if (size >= 1)
            {
                category1Unlocked = true;
                for (int i = 0; i < category1.transform.childCount; i++)
                {
                    category1.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (category2Unlocked == false)
        {
            if (size >= 1.5f)
            {
                category2Unlocked = true;
                for (int i = 0; i < category2.transform.childCount; i++)
                {
                    category2.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (category3Unlocked == false)
        {
            if (size >= 2)
            {
                category3Unlocked = true;
                for (int i = 0; i < category3.transform.childCount; i++)
                {
                    category3.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }

        }
    }


    //Recoger objetos o pick up Sticky objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sticky"))
        {
            // Grow the Sticky Ball
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            size += 0.1f;
            distanceToCamera += 0.09f;
            other.enabled = false;

            // Becomes Child so it stays with the Sticky Ball
            other.transform.SetParent(this.transform);

            // Set the UI Text for Ball Size
            sizeUI.GetComponent<Text>().text = "Mass: " + Math.Round(size, 2).ToString();

            this.GetComponent<AudioSource>().PlayOneShot(pickupSound);

        }

    }
}
