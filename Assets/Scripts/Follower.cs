using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;
    public Vector3 lookDecal;
    public Vector3 followDecal;
    public Rigidbody rb;
    public float speedDecal = 1;
    public float dist = 5;
    public float height = 2;
    
    void Update()
    {
        transform.LookAt(target.TransformPoint(lookDecal));
     
        transform.position = target.position - (target.position - transform.position).normalized * dist;
    }
}
