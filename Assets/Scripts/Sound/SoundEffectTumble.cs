using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundEffectTumble : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public Vector2 pitchRange;
    private AudioSource audioSource;
    private AudioClip audioClip;
    public bool play;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    
    

    public void PlaySound()
    {
        int trackNum = Random.Range(0, audioClips.Count);
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.PlayOneShot(audioClips[trackNum]);
        Debug.Log("Son lancé :" + audioClips[trackNum].name);
    }

    public void StopPlay()
    {
        Debug.Log("Son arrêté");
        audioSource.Stop();
    }
}