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
    public int ignorePercent = 4;
    private int ignoreSize;


    public int maxSpeed = 60;
    public float speedFactor = 1.8f;
    public float brakeFactor = 0.2f;
    public float rotateFactor = 2f;

    [Header("Debug")]
    public bool showDebug = false;
    public Text debugText;
    private bool ignoreStatut1 = true;
    private bool ignoreStatut2 = false;
    public int versionFreins = 1;
    private string explicationFreins;


    private void Start()
    {
        //Recupération du rigidbody.
        rb = gameObject.GetComponent<Rigidbody>();
        //Génère la valeur de la zone a ignorer.
        ignoreSize = scale / (ignorePercent);
        //Affiche le débug ou non.
        debugText.gameObject.SetActive(showDebug);
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
        //Trouve la position sur l'écran par rapport au centre.
        xPosition = Input.mousePosition.x - (Screen.width / 2);
        yPosition = Input.mousePosition.y - (Screen.height / 2);

        //Met les valeurs à l'échelle.
        zSpeed = (int)Math.Ceiling(xPosition / (Screen.width / 2) * scale);
        xSpeed = (int)Math.Ceiling(yPosition / (Screen.height / 2) * scale);

        //Protège des sorties d'écran.
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
        //Si c'est au delà de la zone, le tumble accélère.
        //if (Math.Abs(xSpeed) > ignoreSize) Prends en compte le recul



        if (xSpeed > ignoreSize)
        {
            {
                //Ajoute la force. Distance du centre - la taille du ignoré x l'accélération. Le tout dans la direction de la caméra.
                //Force de type accélartion.

                Vector3 dir = camTransfom.forward;
                rb.AddForce((xSpeed - ignoreSize) * speedFactor * dir, ForceMode.Acceleration);
                ignoreStatut1 = false;
            }
        }
        //Sinon, il freine
        else
        {
            //if (xSpeed > 0 && rb.velocity.magnitude > 0) 
            //{
            //Freine. 

            //Version une, joue sur la vitesse.
            if(versionFreins == 1)
            {
                explicationFreins = "Joue sur la vitesse.";
                Vector3 dir = camTransfom.forward;
                rb.velocity = new Vector3(rb.velocity.x - (brakeFactor * Time.deltaTime), 0, 0);
            }
            //Version 2, ajoute de la force au contraire.
            if (versionFreins == 2)
            {
                explicationFreins = "Ajoute de la force en face, de type accélération.";
                Vector3 dir = camTransfom.forward;
                rb.AddForce((xSpeed - ignoreSize) * brakeFactor * -dir, ForceMode.Acceleration);
            }
            //Version 3, ajoute de la force au contraire.
            if (versionFreins == 3)
            {
                explicationFreins = "Ajoute de la force en face, de type impulsion.";
                Vector3 dir = camTransfom.forward;
                rb.AddForce((xSpeed - ignoreSize) * brakeFactor * -dir, ForceMode.Impulse);
            }
            //}
            ignoreStatut1 = true; 
        }
    }

    void checkMaxSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            //Change la vitesse proprement selon la vitesse maxi.
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void setDirection()
    {
        //Si on est en dehors de la zone.
        if (Math.Abs(zSpeed) > ignoreSize)
        {
            //Ajoute la force. Distance du centre - la taille du ignoré x le facteur. Le tout vers la droite.
            //Force de type accélartion.

            Vector3 dir = camTransfom.right;
            rb.AddForce(zSpeed * rotateFactor * dir, ForceMode.Impulse);

            ignoreStatut2 = false;
        }
        else
        {
            ignoreStatut2 = true;
        }
    }

    void debug()
    {
        if (showDebug)
        {
            debugText.text = "Vitesse: " + rb.velocity.magnitude.ToString() + "  - Zone ignorée " + ignoreSize + System.Environment.NewLine
                + "x: " + xSpeed + " * " + speedFactor + " = " + xSpeed * speedFactor + " | Ignoré: " + ignoreStatut1 + System.Environment.NewLine
                + "z: " + zSpeed + " * " + rotateFactor + " = " + zSpeed * rotateFactor + " | Ignoré: " + ignoreStatut2 + System.Environment.NewLine
                + "Version frein: " + versionFreins + " - " + explicationFreins;
        }
    }

}
