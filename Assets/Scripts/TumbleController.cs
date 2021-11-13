using UnityEngine;
using TMPro;

public class TumbleController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform camTransform;
    private bool isGrounded = true;

    [Header("Valeurs")]
    [SerializeField] private int maxSpeed = 60;
    [SerializeField] private float speedFactor = 7;
    [SerializeField] private float jumpFactor = 2;

    [Header("Debug")]
    [SerializeField] private bool showDebug = false;
    public TextMeshProUGUI debugText;

    private void Start()
    {
        //Recupération du rigidbody.
        rb = gameObject.GetComponent<Rigidbody>();
        debugText.gameObject.SetActive(showDebug);
    }

    private void Update()
    {
        Debug();
    }

    private void FixedUpdate()
    {
        ApplyControl();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    private void ApplyControl()
    {
        Vector3 forwardDir = camTransform.forward;
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z);
        forwardDir = forwardDir.normalized;

        Vector3 rightDir = camTransform.right;
        rightDir = new Vector3(rightDir.x, 0, rightDir.z);
        rightDir = rightDir.normalized;


        rb.AddForce(forwardDir * Input.GetAxis("Vertical") * speedFactor 
                    + rightDir * Input.GetAxis("Horizontal") * speedFactor);

        float mouseDecelration = 1 - Mathf.Abs(Input.GetAxis("Mouse X") * 0.02f);
        rb.velocity = rb.velocity * mouseDecelration;
        
        if (Input.GetKey(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
        
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
    
    private void Debug()
    {
        if (showDebug)
        {
            debugText.text = "Vitesse: " + rb.velocity.magnitude.ToString() + "\n";
        }
    }

}
