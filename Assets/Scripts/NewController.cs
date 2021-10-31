using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewController : MonoBehaviour
{
    [Header("GameObjects")]
    public Transform camTransfom;
    private Rigidbody rb;
    public Transform helper;
    private PhysicMaterial physicMaterial;

    [Header("Valeurs")]
    public Vector3 direction;
    public float moveSpeed = 100;
    private float xPosition;
    private float yPosition;
    private int zSpeed;
    private int xSpeed;
    public int scale = 20;
    public int ignorePercent = 3;
    private int ignoreSize;

    public int speedFactor = 0;
    public float brakeFactor = 0.2f;

    [Header("Debug")]
    public Text vValue;
    public Text yValue;
    private bool ignoreStatut = true;

    private void Start()
    {
        //Recupération du rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
        physicMaterial = gameObject.GetComponent<SphereCollider>().material;
        //Génère la valeur de la zone a ignorer
        ignoreSize = scale / (ignorePercent);
    }

    void Update()
    {

        findCursorPosition();

        direction = transform.position - camTransfom.position;
        //direction.y = +yPosition/70;
        direction.y = 0;
        //direction.z = -xSpeed / 50; 
        direction = direction.normalized;

        helper.position = transform.position;
        helper.LookAt(transform.position + direction);

        //rb.AddForce(Input.GetAxis("Horizontal") * moveSpeed * helper.right +
        //              Input.GetAxis("Vertical") * moveSpeed * helper.forward);

        //camTransfom.position = new Vector3(camTransfom.position.x, camTransfom.position.y, camTransfom.position.z + (xSpeed/50));
        //rb.AddForce(xSpeed/5, 0 ,0);

        setSpeed();

    }

    void findCursorPosition()
    {
        //Trouve la position sur l'écran par rapport au centre
        xPosition = Input.mousePosition.x - (Screen.width / 2);
        yPosition = Input.mousePosition.y - (Screen.height / 2);

        //Passe la valeur de -100 a 100
        zSpeed = (int)Math.Ceiling(xPosition / (Screen.width / 2) * scale);
        xSpeed = (int)Math.Ceiling(yPosition / (Screen.height / 2) * scale);

        //Protège des sorties d'écran
        if (zSpeed > scale) {
            zSpeed = scale;
        }
        if (zSpeed < -scale)
        {
            zSpeed = -scale;
        }
        if (xSpeed > scale)
        {
            xSpeed = scale;
        }
        if (xSpeed < -scale)
        {
            xSpeed = -scale;
        }

        //Debug
        vValue.text = rb.velocity.magnitude.ToString() + " - " + ignoreSize + "  " + ignoreStatut + "  " + physicMaterial.dynamicFriction;
        yValue.text = "x: " + xSpeed +" z: " + zSpeed;
    }

    void setSpeed()
    {
        //Si c'est au delà de la zone, le thumble accélère
        if (Math.Abs(xSpeed) > ignoreSize)
        {
            {
                if (ignoreStatut == true)
                {
                    physicMaterial.dynamicFriction = 0.6f;
                }
                rb.AddForce(xSpeed * speedFactor, 0, 0, ForceMode.Acceleration);
                ignoreStatut = false;
            }
        }
        //Sinon, il freine
        else
        {
            if (ignoreStatut == false)
            {
                rb.AddForce(-rb.velocity.x * brakeFactor, 0, 0, ForceMode.Impulse);
                physicMaterial.dynamicFriction = 2;
            }
            //rb.AddForce(-rb.velocity.x * brakeFactor, 0, 0, ForceMode.Acceleration);
            ignoreStatut = true;
        }
    }
}
