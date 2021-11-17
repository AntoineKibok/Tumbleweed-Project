using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour


{
    private CinemachineFreeLook cam;

    [Header("Camera tilt")]
    public bool applyTilt = true;
    public float tiltFactor = 2f;
    public float smoothTimeTilt = 3f;
    private float tiltValue = 0f;
    private float tiltVelocity = 0.0f;
    
    [Header("Vitesse secoueuse")]
    public bool applyShake = true;
    public bool fakeShake = false;
    public Rigidbody rbPlayer;
    private TumbleController TumbleController;
    public int percentActivation = 80;

    


    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {

        if (applyTilt) 
            ApplyTilt();
        
        if (applyShake)
            ApplyShake();


    }

    private void ApplyTilt()
    {
        float newTiltValue = (Input.GetAxis("Horizontal") * tiltFactor) * -1;
        float smoothTiltValue = Mathf.SmoothDamp(tiltValue, tiltFactor, ref tiltVelocity, smoothTimeTilt, 0.01f);
        
        cam.m_Lens.Dutch = smoothTiltValue;
        
        tiltValue = newTiltValue;
    }
    

    private void ApplyShake()
    {
        int maxSpeed = 10;
        float currentSpeed = rbPlayer.velocity.magnitude;
        
        float shakeFactor = Mathf.Lerp(0, 1, currentSpeed / maxSpeed);

        
        if (currentSpeed > maxSpeed)
        {
            Debug.Log("Shake " + maxSpeed+ "   " + shakeFactor);
            Shake(shakeFactor);
        }

        else
        { 
            Debug.Log("Pas shake " + maxSpeed+ "   " + shakeFactor);
            Shake(0);
        }
    }
    public void Shake(float intensity)
    {
        cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
    }
}
