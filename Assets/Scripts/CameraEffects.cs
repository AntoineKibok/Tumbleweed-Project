using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour


{
    public bool applyTilt = true; 
    private CinemachineFreeLook cam;
    public float tiltFactor = 2f;
    public float smoothTimeTilt = 3f;
    private float tiltValue = 0f;
    private float tiltVelocity = 0.0f;

    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        if (applyTilt) 
            ApplyTilt();
    }

    private void ApplyTilt()
    {
        float newTiltValue = (Input.GetAxis("Horizontal") * tiltFactor) * -1;
        float smoothTiltValue = Mathf.SmoothDamp(tiltValue, tiltFactor, ref tiltVelocity, smoothTimeTilt, 0.01f);
        
        cam.m_Lens.Dutch = smoothTiltValue;
        
        tiltValue = newTiltValue;
    }
}
