using UnityEngine;
using TMPro;

public class TumbleController : MonoBehaviour
{

    private Rigidbody rb;
    private PhysicMaterial physicMaterial;
    private SphereCollider sCollider;
    public Transform camTransform;

    [Header("Valeurs")]
    [SerializeField] private Vector3 direction;
    private float xPosition;
    private float yPosition;
    private float axisRight;
    private float axisForward;
    private float ignoreSize;
    private bool isGrounded = true;
    private float isGroundValue;


    [SerializeField] private int maxSpeed = 60;
    [SerializeField] private float speedFactor = 50;
    [SerializeField] private float brakeFactor = 50;
    [SerializeField] private float rotateFactor = 50;
    [SerializeField] private float jumpFactor = 50;


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
    
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
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
            /*
            debugText.text = "Vitesse: " + rb.velocity.magnitude.ToString() + "  - Zone ignorée " + ignoreSize + "\n"
                + "x: " + axisForward + " * " + speedFactor + " = " + axisForward * speedFactor + " | Ignoré: " + ignoreStatut1 + "\n"
                + "z: " + axisRight + " * " + rotateFactor + " = " + axisRight * rotateFactor + " | Ignoré: " + ignoreStatut2 + "\n"
                + "Version frein: " + versionFreins + " - " + explicationFreins;
                */
        }
    }

}
