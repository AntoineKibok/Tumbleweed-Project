using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundFromObject : MonoBehaviour
{
    // Start is called before the first frame update

    public List<AudioClip> audioClips;
    private AudioSource source;

    void Start()
    {
         source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        source.clip = audioClips[Random.Range(0, audioClips.Count)];
        source.Play();
    }
}
