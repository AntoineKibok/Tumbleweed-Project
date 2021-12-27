using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool canMove = true;
    public bool cinematicSaloon = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void confirmStep(string step)
    {
        switch (step)
        {
            case "saloon":
                cinematicSaloon = true;
                Debug.Log(step + " confirmée.");
                break;
            
            default:
                Debug.Log("Mauvaise entrée");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
