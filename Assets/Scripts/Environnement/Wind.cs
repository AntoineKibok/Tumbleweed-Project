using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Wind : MonoBehaviour
{

    public float windStrengh = 10;
    private Vibrations vibrations;


    void Start()
    {
        vibrations = GameObject.Find("GameManager").GetComponent<Vibrations>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<TumbleController>() != null)
        {
            other.GetComponent<Rigidbody>().AddForce(transform.up * windStrengh);
            GamePad.SetVibration(vibrations.playerIndex,0.05f,0);
        }
    }

}
