using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundAmbiant : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
 
    private AudioSource audioSource;
    private AudioClip audioClip;
    public bool play;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (play)
        {
            PlaySound();
        }
    }

    private void Update()
    {
        if (play)
        {
            if (!audioSource.isPlaying)
            {
                PlaySound();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                StopPlay();
            }
        }
    }

    void PlaySound()
    {
        int trackNum = Random.Range(0, audioClips.Count);
        audioSource.PlayOneShot(audioClips[trackNum]);
    }

    public void StopPlay()
    {
        audioSource.Stop();
    }
}