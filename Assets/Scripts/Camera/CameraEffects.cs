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
    public int percentActivation = 70;


    [Header("Camera tilt")]
    public bool applyTilt = true;
    public float tiltFactor = 3f;
    public float cameraTilt;
    public float cameraTiltLerpSpeed = 0.02f;
    
    [Header("Vitesse secoueuse")]
    public bool applyShake = true;
    private Rigidbody rbPlayer;
    private int speedLimit;
    public float shakeFactor = 1;
    private float speedEffectStart, currentSpeed;
    
    [Header("Debug")]
    public bool showDebug;
    public TextMeshProUGUI debugText;
    public Slider debugSlider;

    private void checkDebug()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            showDebug = !showDebug; 
            debugText.gameObject.SetActive(showDebug);
        }
    }

    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
        
        speedLimit = GameObject.FindWithTag("Player").GetComponent<TumbleController>().maxSpeed;
        rbPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        if (applyShake)
        {
            speedEffectStart = speedLimit * (percentActivation * 0.01f);
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

    public float GetSpeedEffectIntensity()
    {
        currentSpeed = rbPlayer.velocity.magnitude; 
        if (currentSpeed > speedEffectStart) 
        { 
            return (((currentSpeed - speedLimit) / (speedEffectStart - speedLimit)) * -shakeFactor + 1);
        }
        else
        { 
            return 0;
        }
        
    }



        private void ApplyShake()
    {
        Shake(GetSpeedEffectIntensity());
        
        if (showDebug)
        {
            debugSlider.value = Mathf.Clamp01(currentSpeed/speedLimit);
            debugText.text = "Current: " + currentSpeed + " / " + speedLimit + "   " +
                             "\n" + "StartShake: " + speedEffectStart +
                             "\n" + "ShakeFactor: " + shakeFactor;
        }
        
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
