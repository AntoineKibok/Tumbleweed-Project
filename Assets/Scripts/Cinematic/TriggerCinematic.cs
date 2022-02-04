using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class TriggerCinematic : MonoBehaviour
{
    public PlayableDirector director;
    public SphereCollider collider;
    public TumbleController controller;
    public GameManager manager;
    public string step;
    public Volume PlayVolume, CinematicVolume;
    public GameObject highlight;
    public bool manualStart = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre dans la zone");
        if (manager.canAnimationStart(step))
        {
            director.Play();
        }
    }

    private void Update()
    {
        if (manager.canAnimationStart(step))
        {
            if (manualStart)
            {
                director.Play();
            }
        }
    }

    //Quand l'animation est lancée
    void OnPlayableDirectorPlayed(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("Cinématique " + aDirector.name + " lancée.");
            manager.canMove = false;
            manager.cinematicSaloon = true;

            if (PlayVolume != null)
            {
                PlayVolume.gameObject.SetActive(false);

            }
            if (CinematicVolume != null)
            {
                CinematicVolume.gameObject.SetActive(true);
            }

        }
    }
    
    //Quand l'animation s'arrête
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("Cinématique " + aDirector.name + " terminée.");
            manager.canMove = true;
            manager.confirmStep(step);
            if (CinematicVolume != null)
            {
                CinematicVolume.gameObject.SetActive(false);
            }

            if (PlayVolume != null)
            {
                PlayVolume.gameObject.SetActive(true);

            }
            highlight.SetActive(false);
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
