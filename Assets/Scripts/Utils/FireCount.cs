using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCount : MonoBehaviour
{
    public int fireCount = 0;
    public int totalCount = 1584;
    public GameObject burntField;

    public void AddFire()
    {
        fireCount++;

        if (fireCount == 2)
        {
            GetComponent<AudioSource>().Play();
        }
        
        if (fireCount >= totalCount)
        {
            GetComponent<AudioSource>().Stop();
            burntField.GetComponent<TriggerCinematic>().manualStart=true;
        } 
    }
    
}
