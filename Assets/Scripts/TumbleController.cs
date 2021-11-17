using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class TumbleController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform camTransform;
    private BigColliderUtil BigColliderUtil;
    public SphereCollider colliderBigger;
    public bool isGrounded = true;
    public bool isBigColliding = true;

    [Header("Valeurs")]
    [SerializeField] public int maxSpeed = 60;
    [SerializeField] private float speedFactor = 7;
    [SerializeField] private float jumpFactor = 2;
    public bool flyingMode = false;

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
        //Initialise les valeurs
        Vector3 forwardDir = camTransform.forward;
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z);
        forwardDir = forwardDir.normalized;

        Vector3 rightDir = camTransform.right;
        rightDir = new Vector3(rightDir.x, 0, rightDir.z);
        rightDir = rightDir.normalized;


        //Déplace en fonction des contrôles
        rb.AddForce(forwardDir * Input.GetAxis("Vertical") * speedFactor 
                    + rightDir * Input.GetAxis("Horizontal") * speedFactor);

        //Baisse de la vitesse quand caméra bouge (bug fix)
        float mouseDecelration = 1 - Mathf.Abs(Input.GetAxis("Mouse X") * 0.02f);
        rb.velocity = rb.velocity * mouseDecelration;

        //Saute
        Jump(jumpFactor, forwardDir);
        
        //Contrôle la vitesse
        ClampSpeed();


    }

    public void Jump(float factor, Vector3 forwardDir)
    {
        if (Input.GetAxis("Jump") != 0 && isGrounded)
        {
            //Ajoute un petit booste de vitesse
            if (rb.velocity.magnitude > 0.3f)
            {
                rb.AddForce(forwardDir * (jumpFactor/2), ForceMode.Impulse);
            }
            
            //Saute
            rb.AddForce(Vector3.up * jumpFactor, ForceMode.Impulse);
        }


        if (flyingMode)
        {
            if (Input.GetAxis("Jump") != 0 && !isGrounded)
            {
                rb.useGravity = false;
            }
            else
            {
                rb.useGravity = true;
            }
        }
        
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
            debugText.text = "Vitesse: " + rb.velocity.magnitude.ToString() + "\n" +
                rb.velocity.magnitude +"   +   " + ((60 / 100) * 50);
        }
    }

}
