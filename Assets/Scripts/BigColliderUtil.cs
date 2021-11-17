using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigColliderUtil : MonoBehaviour
{
    public bool isColliding;
    public SphereCollider collider;
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");

        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
        }
        
    }

    private void Start()
    {
        Debug.Log(30 / 100 * 50);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
        }
    }
    
}
