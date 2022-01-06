using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IgnitePlayer : MonoBehaviour {

    public bool active;                 //Is this timer active?
    public float cooldown;              //How often this cooldown may be used
    public float timer;                 //Time left on timer, can be used at 0
    public float length = 60;
    public GameObject fire;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Faya baby");
            Ignite();
        }
    }
    
    private void CollideDetection()
    {
        RaycastHit hit;
        Vector3 p1 = transform.position;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, 1, transform.forward, out hit, 10))
        {
            if (hit.transform.tag == "Fire")
            {
                Ignite();
            }
        }
    }

    private void Ignite()
    {
        Debug.Log("Faya");
        fire.SetActive(true);
        timer = length;
        active = true;
    }

    private void Extinguish()
    {
        active = false;
        Debug.Log("Plus faya");
        fire.SetActive(false);
    }

    private void CorrectScale()
    {
        float scale = Mathf.InverseLerp(0, length, timer);
        fire.transform.localScale = new Vector3(scale,scale,scale);
    }
    
    void Update () {
        
        CollideDetection();
        
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