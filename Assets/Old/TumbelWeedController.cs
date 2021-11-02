using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbelWeedController : MonoBehaviour
{
    public Vector3 direction;
    public Transform camTransfom;
    public Rigidbody rb;
    public Transform helper;
    public float moveSpeed = 100;

    void Update()
    {
        direction = transform.position - camTransfom.position;
        direction.y = 0;
        direction = direction.normalized;

        helper.position = transform.position;
        helper.LookAt(transform.position + direction);

        rb.AddForce(Input.GetAxis("Horizontal") * moveSpeed * helper.right +
                      Input.GetAxis("Vertical") * moveSpeed * helper.forward);
    }
}
