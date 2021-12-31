using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IgnitePlayer : MonoBehaviour {

    public bool active;                 //Is this timer active?
    public float cooldown;              //How often this cooldown may be used
    public float timer;                 //Time left on timer, can be used at 0
    public float length = 60;
    public SphereCollider collider;
    
    public GameObject fireOnPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Faya baby");
            Ignite();
        }
    }

    private void Ignite()
    {
        fireOnPlayer.SetActive(true);
        timer = length;
        active = true;
    }

    private void Extinguish()
    {
        active = false;
        Debug.Log("Plus faya");
        fireOnPlayer.SetActive(false);
    }
    
    void Update () {
        if (active)
        {
            timer -= Time.deltaTime;    //Subtract the time since last frame from the timer.
            Debug.Log(timer);

        }

        if (timer < 0)
        {
            timer = 0;                  //If timer is less than 0, reset it to 0 as we don't want it to be negative
            Extinguish();
        }
    }
    
}