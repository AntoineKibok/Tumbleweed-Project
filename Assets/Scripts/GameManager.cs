using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool canMove = true;
    public bool cinematicIntro = false;
    public bool cinematicArrival = false;
    public bool cinematicSaloon = false;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    public void confirmStep(string step)
    {
        switch (step)
        {
            case "intro":
                cinematicIntro = true;
                Debug.Log(step + " confirmée.");
                break;

            case "arrival":
                cinematicArrival = true;
                Debug.Log(step + " confirmée.");
                break;

            case "saloon":
                cinematicSaloon = true;
                Debug.Log(step + " confirmée.");
                break;

            default:
                Debug.Log("Mauvaise entrée");
                break;
        }
    }

    public bool canAnimationStart(string step)
    {
        switch (step)
        {
            case "intro":
                return !cinematicIntro;
                break;

            case "arrival":
                return !cinematicArrival;
                break;

            case "saloon":
                return !cinematicSaloon;
                break;

            default:
                return true;
                break;
        }

        return true;
    }


    // Update is called once per frame
    void Update()
    {
    }
}
