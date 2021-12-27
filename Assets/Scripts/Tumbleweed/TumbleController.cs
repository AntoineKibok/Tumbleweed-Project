using UnityEngine;
using TMPro;

public class TumbleController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform camTransform;
    public bool isGrounded = true;
    public GameManager manager;

    [Header("Valeurs")]
    public int maxSpeed = 60;
    [SerializeField] private float speedFactor = 7;
    [SerializeField] private float jumpFactor = 2;

    [Header("Debug")]
    [SerializeField] private bool showDebug = false;
    public TextMeshProUGUI debugText;

    public float rayLenght = 1;
    public float energy = 0;
    public float energyMax = 100;
    public float energyDrain = 1;
    public float enregisedJumpStrengh = 1;
    public bool canMove = true;


    private void Start()
    {
        //Recupération du rigidbody.
        rb = gameObject.GetComponent<Rigidbody>();
        //debugText.gameObject.SetActive(showDebug);
    }

    private void Update()
    {
        Debug();
        checkExit();
        checkDebug();
        GroundDetection();
    }

    private void FixedUpdate()
    {
        if (manager.canMove)
        {
            ApplyControl();
        }
        else
        { 
            rb.velocity = rb.velocity/1.1f;
        }

    }

    private void checkExit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    private void checkDebug()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {   
            showDebug = !showDebug;
            debugText.gameObject.SetActive(showDebug);
        }
    }

    private void GroundDetection()
    {
        Vector3 origin = transform.position;
        Vector3 dir = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, rayLenght))
        {
            UnityEngine.Debug.DrawRay(origin,dir * hit.distance, Color.green);
            isGrounded = true;
            energy = energyMax;
        }
        else
        {
            UnityEngine.Debug.DrawRay(origin,dir * rayLenght, Color.red);
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
            //JumpEffect(forwardDir);
        }


        if (energy > 0)
        {
            if (Input.GetAxis("Jump") != 0)
            {
                energy -= energyDrain;
                rb.AddForce(Vector3.up * enregisedJumpStrengh);
            }
        }
        
    }

    //Ne fonctionne pas encore, problèmes a cause de la rotation
    public void JumpEffect(Vector3 forwardDir)
    {
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y-0.01f,transform.localScale.x);

    }
    
    private void ClampSpeed()
    {
        if (rb.velocity.magnitude >= maxSpeed)
        {
            //Change la vitesse proprement selon la vitesse maxi.
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    
    private void Debug()
    {
        
        if (showDebug)
        {
            debugText.text = "";
        }

    }
}
