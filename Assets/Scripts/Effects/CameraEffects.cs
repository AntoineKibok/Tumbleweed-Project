using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;


public class CameraEffects : MonoBehaviour


{
    private CinemachineFreeLook cam;
    [Range(0, 100)]
    public int percentActivation = 20;


    [Header("Camera tilt")]
    public bool applyTilt = true;
    public float tiltFactor = 3f;
    public float cameraTilt;
    public float cameraTiltLerpSpeed = 0.02f;
    
    private Rigidbody rbPlayer;
    private int speedLimit;
    private float speedEffectStart, currentSpeed;

    public GameManager manager;
    
    [Header("Vitesse secoueuse")]
    public bool applyShake = true;
    public float shakeFactor = 1;
    
    [Header("Modification FOV")]
    public bool applyFOV = true;
    public float FOVInitial = 40;
    [Range(40, 120)] 
    public float FOVMax = 60;
    public float FOVLerpSpeed = 0.01f;
    public float FOVFactor;
    


    private void ApplyFOV()
    {
        float effectIntensity = GetSpeedEffectIntensity(FOVFactor);
        float newFOV = 0, oldFOV = 0, lerpedFOV = 0;

        if (currentSpeed > speedEffectStart)
        {
            oldFOV = cam.m_Lens.FieldOfView;
            newFOV = Mathf.Lerp(FOVInitial, FOVMax, effectIntensity);
            lerpedFOV = Mathf.Lerp(oldFOV, newFOV, FOVLerpSpeed);
            
            cam.m_Lens.FieldOfView = lerpedFOV;
        }

    }

    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
        
        speedLimit = GameObject.FindWithTag("Player").GetComponent<TumbleController>().maxSpeed;
        rbPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        if (applyShake || applyFOV)
        {
            speedEffectStart = speedLimit * (percentActivation * 0.01f);
        }
    }

    private void Update()
    {
        if (manager.canMove)
        {
            if (applyTilt) 
                ApplyTilt();
        
            if (applyShake)
                ApplyShake();

            if (applyFOV)
                ApplyFOV();
        }
    }

    private void ApplyTilt()
    {
        float newTiltValue = (Input.GetAxis("Horizontal") * tiltFactor) * -1;
        cameraTilt = Mathf.Lerp(cameraTilt, newTiltValue, cameraTiltLerpSpeed);
        cam.m_Lens.Dutch = cameraTilt;
    }

    public float GetSpeedEffectIntensity(float factor)
    {
        currentSpeed = rbPlayer.velocity.magnitude; 
        if (currentSpeed > speedEffectStart) 
        { 
            return (((currentSpeed - speedLimit) / (speedEffectStart - speedLimit)) * -factor + 1);
        }
        else
        { 
            return 0;
        }
        
    }



        private void ApplyShake()
    {
        Shake(GetSpeedEffectIntensity(shakeFactor));

    }
    public void Shake(float intensity)
    {
        cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        
        cam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = intensity;
        cam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = intensity;
        cam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = intensity;
    }

}
