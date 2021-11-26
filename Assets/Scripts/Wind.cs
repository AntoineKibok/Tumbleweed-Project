using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    public float windStrengh = 10;
    

    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<TumbleController>() != null)
        {
            other.GetComponent<Rigidbody>().AddForce(transform.up * windStrengh);
        }
    }

}
