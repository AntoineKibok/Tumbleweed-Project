using UnityEngine;
using TMPro;

public class TumbleController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform camTransform;
    public bool isGrounded = true;
    public GameManager manager;
    public SoundEffectTumble soundEffect;

    [Header("Valeurs")]
    public int maxSpeed = 60;
    [SerializeField] private float speedFactor = 7;
    [SerializeField] private float jumpFactor = 2;
    

    public float rayLenght = 1;
    public float energy = 0;
    public float energyMax = 100;
    public float energyDrain = 1;
    public float enregisedJumpStrengh = 1;

    private void Start()
    {
        //Recupération du rigidbody.
        rb = gameObject.GetComponent<Rigidbody>();
        soundEffect = GetComponent<SoundEffectTumble>();
        //debugText.gameObject.SetActive(showDebug);
    }

    private void Update()
    {
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
    

    private void GroundDetection()
    {
        Vector3 origin = transform.position;
        Vector3 dir = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, rayLenght))
        {
            UnityEngine.Debug.DrawRay(origin,dir * hit.distance, Color.green);
            if (!isGrounded)
            {
                soundEffect.PlaySound();
            }
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
            //Ajoute un petit boost de vitesse
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

    private void ClampSpeed()
    {
        if (rb.velocity.magnitude >= maxSpeed)
        {
            //Change la vitesse proprement selon la vitesse maxi.
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    
}
