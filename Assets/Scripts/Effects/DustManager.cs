using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustManager : MonoBehaviour
{
    public ParticleSystem ps;
    public Gradient gradient;
    public Rigidbody rb;
    public float maxSpeed = 2;
    public TumbleController Controller;

  public ParticleSystem.MainModule mainModule;
    // Start is called before the first frame update
    void Update()
    {
        if (Controller.isGrounded == false)
        {
            if(ps.isPaused == false)
            {
                ps.Pause();
                ps.Clear();
            }
        }
        else
        {
            if (ps.isPaused)
            {
                ps.Play();
            }
        }
            
        mainModule = ps.main;
        mainModule.startColor = new Color(1,1,1, rb.velocity.magnitude/maxSpeed);

    }
}
