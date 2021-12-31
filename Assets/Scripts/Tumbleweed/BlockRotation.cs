using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    
    /*
    private Rigidbody rb;
 
    private void Awake() {
        rb = GetComponentInParent<Rigidbody>();
    }
    private void FixedUpdate() {
        var rotation = Quaternion.LookRotation(Vector3.up , Vector3.forward);
        rb.rotation = rotation;
    }
    */

    public Transform target;
    private Transform tr;

    public void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        tr.position = target.position;
    }
}
