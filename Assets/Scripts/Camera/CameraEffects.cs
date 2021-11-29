using System;
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
    public float tiltFactor = 3f;
    public float cameraTilt;
    public float cameraTiltLerpSpeed = 0.02f;
    
    [Header("Vitesse secoueuse")]
    public bool applyShake = true;
    private Rigidbody rbPlayer;
    public int percentActivation = 70;
    private int speedLimit;
    private float shakeStart, shakeFactor, currentSpeed;
    
    [Header("Debug")]
    public bool showDebug;
    public TextMeshProUGUI debugText;
    
    private void checkDebug()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            showDebug = !showDebug;

            if (showDebug == true)
            {
                debugText.gameObject.SetActive(true);
            }
            
            if (showDebug == false)
            {
                debugText.gameObject.SetActive(false);
            }

        }
    }

    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
        
        speedLimit = GameObject.FindWithTag("Player").GetComponent<TumbleController>().maxSpeed;
        rbPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

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
        
        checkDebug();
    }

    private void ApplyTilt()
    {
        float newTiltValue = (Input.GetAxis("Horizontal") * tiltFactor) * -1;
        cameraTilt = Mathf.Lerp(cameraTilt, newTiltValue, cameraTiltLerpSpeed);
        cam.m_Lens.Dutch = cameraTilt;
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
        
        cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = intensity*1.5f;
        cam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = intensity*1.5f;
        cam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = intensity*1.5f;
    }

}
