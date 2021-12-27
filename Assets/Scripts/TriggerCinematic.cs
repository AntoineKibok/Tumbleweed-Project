using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCinematic : MonoBehaviour
{
    public PlayableDirector director;
    public SphereCollider collider;
    public TumbleController controller;
    public GameManager manager;
    public string step;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre dans la zone");
        director.Play();
    }

    void OnPlayableDirectorPlayed(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("Cinématique " + aDirector.name + " lancée.");
            manager.canMove = false;
            manager.cinematicSaloon = true;
        }
    }
    
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("Cinématique " + aDirector.name + " terminée.");
            manager.canMove = true;
            manager.confirmStep(step);
        }
    }
    
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
        director.played += OnPlayableDirectorPlayed;
    }
    
    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
        director.played -= OnPlayableDirectorPlayed;
    }

}
