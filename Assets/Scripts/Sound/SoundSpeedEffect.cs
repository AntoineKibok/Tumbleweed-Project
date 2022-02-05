using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundSpeedEffect : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public Vector2 pitchRange;
    private AudioSource audioSource;
    private AudioClip audioClip;
    public bool play;
    public Rigidbody rb;
    private int interval = 1; 
    private float nextTime = 0;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    

         
    // Update is called once per frame
    void Update () {
        
        
        
     
        if (Time.time >= nextTime) {
     
            //do something here every interval seconds
     
            nextTime += interval; 
     
        }
         
    }
    

    public void PlaySound()
    {
        int trackNum = Random.Range(0, audioClips.Count);
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.PlayOneShot(audioClips[trackNum]);
    }

    public void StopPlay()
    {
        audioSource.Stop();
    }
}