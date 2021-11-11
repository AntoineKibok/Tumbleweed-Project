using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TumbleController : MonoBehaviour
{
    //[Header("GameObjects")]
    //[SerializeField] private Transform camTransfom;
    private Rigidbody rb;
    //[SerializeField] private Transform helper;
    private PhysicMaterial physicMaterial;
    private SphereCollider sCollider;

    [Header("Valeurs")]
    [SerializeField] private Vector3 direction;
    private float xPosition;
    private float yPosition;
    private float axisRight;
    private float axisForward;
    //public int scale = 20;
    //public float ignorePercent = 4;
    private float ignoreSize;


    [SerializeField] private int maxSpeed = 60;
    [SerializeField] private float speedFactor = 50;
    [SerializeField] private float brakeFactor = 50;
    [SerializeField] private float rotateFactor = 50;
    [SerializeField] private float jumpFactor = 50;


    [Header("Debug")]
    [SerializeField] private bool showDebug = false;
    public TextMeshProUGUI debugText;
    //private bool ignoreStatut1 = true;
    //private bool ignoreStatut2 = false;
    //[SerializeField] private int versionFreins = 1;
    //private string explicationFreins;


    private void Start()
    {
        //Recupération du rigidbody.
        rb = gameObject.GetComponent<Rigidbody>();
        //Génère la valeur de la zone a ignorer.
        //ignoreSize = scale / (ignorePercent);
        //Affiche le débug ou non.
        debugText.gameObject.SetActive(showDebug);
    }

    private void Update()
    {
        //DirCamera();
        Debug();
    }

    private void FixedUpdate()
    {
        ApplyControl();
        //CheckControl();
        //SetDirection();
        //SetSpeed();
        
        if (Input.GetButton("Jump"))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            rb.AddForce(transform.up * 20f);
        }
    }
    
    /*
    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, helper.transform.position);
    }
    */


    private void ApplyControl()
    {
        
        //rb.AddForce(Input.GetAxis("Horizontal") * rotateFactor * helper.right +
        //            Input.GetAxis("Vertical") * speedFactor * helper.forward);
        
        if (Input.GetKey(KeyCode.UpArrow)) 
            rb.AddForce(Vector3.forward * speedFactor);
		
        if (Input.GetKey(KeyCode.DownArrow)) 
            rb.AddForce(Vector3.back * brakeFactor);

        if (Input.GetKey(KeyCode.LeftArrow)) 
            rb.AddForce(Vector3.left * rotateFactor);
		
        if (Input.GetKey(KeyCode.RightArrow)) 
            rb.AddForce(Vector3.right * rotateFactor);

        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpFactor);
        

        ClampSpeed();
    }
    
    private void ClampSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            //Change la vitesse proprement selon la vitesse maxi.
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    
    /*
    private void DirCamera()
    {
        direction = transform.position - camTransfom.position;
        direction.y = 0;
        direction = direction.normalized;

        helper.position = transform.position;
        helper.LookAt(transform.position + direction);
    }
    


    private void CheckControl()
    {
        //Trouve la position sur l'écran par rapport au centre.
        Vector2 mousePos = Input.mousePosition;
        axisRight = Mathf.Lerp(-scale, scale, mousePos.x / Screen.width);
        axisForward = Mathf.Lerp(-scale, scale, mousePos.y / Screen.height);
    }




    private void SetSpeed()
    {
        //Si c'est au delà de la zone, le tumble accélère.
        //if (Math.Abs(xSpeed) > ignoreSize) Prends en compte le recul



        if (axisForward > ignoreSize)
        {
            {
                //Ajoute la force. Distance du centre - la taille du ignoré x l'accélération. Le tout dans la direction de la caméra.
                //Force de type accélartion.
                Vector3 dir = transform.forward;
                rb.AddForce((axisForward - ignoreSize) * speedFactor * dir, ForceMode.Acceleration);
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
                rb.velocity = new Vector3(rb.velocity.x - (brakeFactor * Time.fixedDeltaTime), 0, 0);
            }
            //Version 2, ajoute de la force au contraire.
            if (versionFreins == 2)
            {
                explicationFreins = "Ajoute de la force en face, de type accélération.";
                Vector3 dir = camTransfom.forward;
                rb.AddForce((axisForward - ignoreSize) * brakeFactor * (dir * -1), ForceMode.Acceleration);
            }
            //Version 3, ajoute de la force au contraire.
            if (versionFreins == 3)
            {
                explicationFreins = "Ajoute de la force en face, de type impulsion.";
                Vector3 dir = camTransfom.forward;
                rb.AddForce((axisForward - ignoreSize) * brakeFactor * (dir * -1), ForceMode.Impulse);
            }
            //}
            ignoreStatut1 = true; 
        }
    }

    private void SetDirection()
    {
        //Si on est en dehors de la zone.
        if (Math.Abs(axisRight) > ignoreSize)
        {
            //Ajoute la force. Distance du centre - la taille du ignoré x le facteur. Le tout vers la droite.
            //Force de type accélartion.
            //
            //Vector3 dir = camTransfom.right;
            //rb.AddForce(zSpeed * rotateFactor * dir, ForceMode.Impulse);
            //

            Quaternion newRotation = transform.rotation;
            newRotation.eulerAngles += new Vector3(0,rotateFactor * axisRight * Time.fixedDeltaTime, 0);
            transform.rotation = newRotation;

            ignoreStatut2 = false;
        }
        else
        {
            ignoreStatut2 = true;
        }
    }
    
    */

    private void Debug()
    {
        if (showDebug)
        {
            /*
            debugText.text = "Vitesse: " + rb.velocity.magnitude.ToString() + "  - Zone ignorée " + ignoreSize + "\n"
                + "x: " + axisForward + " * " + speedFactor + " = " + axisForward * speedFactor + " | Ignoré: " + ignoreStatut1 + "\n"
                + "z: " + axisRight + " * " + rotateFactor + " = " + axisRight * rotateFactor + " | Ignoré: " + ignoreStatut2 + "\n"
                + "Version frein: " + versionFreins + " - " + explicationFreins;
                */
        }
    }

}
