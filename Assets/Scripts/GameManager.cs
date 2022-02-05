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
    public bool cinematicBurnt = false;
    public bool cinematicHorse = false;
    public bool cinematicDuel = false;



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
            
            case "burnt":
                cinematicBurnt = true;
                Debug.Log(step + " confirmée.");
                break;
            
            case "horses":
                cinematicHorse = true;
                Debug.Log(step + " confirmée.");
                break;
            
            case "duel":
                cinematicDuel = true;
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

            case "arrival":
                if (cinematicIntro)
                {
                    return !cinematicArrival;
                }
                else
                {
                    return false;
                }

            case "saloon":
                if (cinematicArrival)
                {
                    return !cinematicSaloon;
                }
                else
                {
                    return false;
                }
            
            case "burnt":
                if (cinematicSaloon)
                {
                    return !cinematicBurnt;
                }
                else
                {
                    return false;
                }
            
            case "horses":
                if (cinematicSaloon)
                {
                    return !cinematicHorse;
                }
                else
                {
                    return false;
                }
            
            case "duel":
                if (cinematicHorse && cinematicBurnt)
                {
                    return !cinematicHorse;
                }
                else
                {
                    return false;
                }

            default:
                return true;
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
