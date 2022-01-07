using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IgnitePlayer : MonoBehaviour {

    public bool active;                 //Is this timer active?
    public float cooldown;              //How often this cooldown may be used
    public float timer;                 //Time left on timer, can be used at 0
    public float length = 60;
    public GameObject fire;
    public GameObject player;

 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.Log("Faya baby");
            Ignite();
        }
    }
    

    private void Ignite()
    {
        Debug.Log("Faya");
        fire.SetActive(true);
        timer = length;
        active = true;
        player.GetComponent<Flamable>().inFlame = true;
        player.GetComponent<Flamable>().PlayerPropagation();

    }

    private void Extinguish()
    {
        player.GetComponent<Flamable>().inFlame = false;

        active = false;
        Debug.Log("Plus faya");
        fire.SetActive(false);
    }

    private void CorrectScale()
    {
        float scale = Mathf.InverseLerp(0, length, timer);
        fire.transform.localScale = new Vector3(scale/2,scale/2,scale/2);
    }
    
    void Update () {
        
        //CollideDetection();
        
        if (active)
        {
            CorrectScale();
            timer -= Time.deltaTime;    //Subtract the time since last frame from the timer.
            //Debug.Log(timer);

        }

        if (timer < 0)
        {
            timer = 0;                  //If timer is less than 0, reset it to 0 as we don't want it to be negative
            Extinguish();
        }
    }
    
}