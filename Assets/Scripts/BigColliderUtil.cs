using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigColliderUtil : MonoBehaviour
{
    public bool isColliding;
    public SphereCollider bigCollider;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
        }
        
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
        }
    }
    
}
