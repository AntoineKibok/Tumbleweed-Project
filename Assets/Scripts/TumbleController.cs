using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TumbleController : MonoBehaviour
{
    [Header("GameObjects")]
    public Transform camTransfom;
    private Rigidbody rb;
    public Transform helper;
    private PhysicMaterial physicMaterial;
    private SphereCollider sCollider;

    [Header("Valeurs")]
    public Vector3 direction;
    private float xPosition;
    private float yPosition;
    private int zSpeed;
    private int xSpeed;
    public int scale = 20;
    public int ignorePercent = 5;
    private int ignoreSize;


    public int maxSpeed = 60;
    public float speedFactor = 0.7f;
    public float brakeFactor = 1.2f;
    public float rotateFactor = 1f;

    [Header("Debug")]
    public bool showDebug;
    public Text line1;
    public Text line2;
    public Text line3;
    private bool ignoreStatut = true;

    private void Start()
    {
        //Recupération du rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
        //Génère la valeur de la zone a ignorer
        ignoreSize = scale / (ignorePercent);

        //Masque le débug
        if (!showDebug)
        {
            line1.text = "";
            line2.text = "";
            line3.text = "";
        }
    }

    void Update()
    {
        findCursorPosition();
        dirCamera();
        setSpeed();
        checkMaxSpeed();
        setDirection();
        debug();
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
        if (zSpeed > scale) 
        {
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
    }

    void dirCamera()
    {
        direction = transform.position - camTransfom.position;
        direction.y = 0;
        direction = direction.normalized;

        helper.position = transform.position;
        helper.LookAt(transform.position + direction);
    }

    void setSpeed()
    {
        //Si c'est au delà de la zone, le thumble accélère
        //if (Math.Abs(xSpeed) > ignoreSize) Prends en compte le recul
        if (xSpeed > ignoreSize)
        {
            {
                Vector3 dir = camTransfom.forward;
                rb.AddForce((xSpeed - ignoreSize) * speedFactor * dir, ForceMode.Acceleration);

                ignoreStatut = false;
            }
        }
        //Sinon, il freine
        else
        {
            if (xSpeed > 0 && rb.velocity.magnitude > 0) 
            {
                rb.velocity = new Vector3(rb.velocity.x - (brakeFactor * Time.deltaTime), 0, 0);
            }
            ignoreStatut = true; 
        }
    }

    void checkMaxSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void setDirection()
    {
        if (Math.Abs(zSpeed) > ignoreSize)
        {
            Vector3 dir = camTransfom.right;
            rb.AddForce(zSpeed * rotateFactor * dir, ForceMode.Acceleration);
        }
    }

    void debug()
    {
        if (showDebug)
        {
            line1.text = "Vitesse: " + rb.velocity.magnitude.ToString() + "  - Zone ignorée " + ignoreSize + ": " + ignoreStatut;
            line2.text = "x: " + xSpeed + " * " + speedFactor +" = " + xSpeed*speedFactor;
            line3.text = "z: " + zSpeed + " * " + rotateFactor + " = " + zSpeed * rotateFactor;
        }
    }

}
