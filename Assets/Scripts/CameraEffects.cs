using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;



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
    public int percentActivation = 80;
    public int speedLimit;
    private float shakeStart, shakeFactor, currentSpeed;
    
    [Header("Debug")]
    public bool showDebug;
    public TextMeshProUGUI debugText;

    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
        
        speedLimit = GameObject.Find("Player").GetComponent<TumbleController>().maxSpeed;

        if (applyShake)
        {
            shakeStart = speedLimit * (percentActivation * 0.01f);
        }
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

        currentSpeed = rbPlayer.velocity.magnitude;

        if (currentSpeed > shakeStart)
        {
            shakeFactor = ((currentSpeed - speedLimit ) / (shakeStart - speedLimit)) * -1 + 1 ;
            Shake(shakeFactor);
        }

        else
        {
            shakeFactor = 0;
            Shake(0);
        }
        
        
        if (showDebug)
        {
            debugText.text = "Current: " + currentSpeed + " / " + speedLimit +
                             "\n" + "StartShake: " + shakeStart +
                             "\n" + "ShakeFactor: " + shakeFactor;
        }
        
    }
    public void Shake(float intensity)
    {
        cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
    }

}
