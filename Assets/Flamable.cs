using System.Collections;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public bool inFlame;
    public bool hasBeenInFlame = false;
    public int lifetime = 15;
    public int iterationFire = 0;
    public GameObject firePrefab;
    private GameObject fireInstance;
    public Vector3 decal;
    public float range = 3;
    public float propagateDelay = 3f;
    public Color endColor;
    public FireCount count;
    
    public void Ignite()
    {
        if (inFlame == false)
        {
            inFlame = true;
            count.AddFire();
            fireInstance = Instantiate(firePrefab, transform.position + decal, Quaternion.Euler(-90,0,0));
            StartCoroutine(Propagate());
        }
    }

    public void PlayerPropagation()
    {
        StartCoroutine(Propagate());
    }

    public IEnumerator Propagate()
    {
        Vector3 pose = Random.insideUnitSphere * range;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position + pose + Vector3.up*10, Vector3.down,out hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<Flamable>() != null)
            {
                if (hit.collider.gameObject.GetComponent<Flamable>().inFlame == false)
                {
                    if (hit.collider.gameObject.GetComponent<Flamable>().hasBeenInFlame == false)
                    {
                        hit.collider.gameObject.GetComponent<Flamable>().Ignite();
                    }
                }
            }
        }

        yield return new WaitForSeconds(propagateDelay);

        iterationFire++;

        if (iterationFire >= lifetime)
        {
            if (inFlame == true)
            {
                StopFire();
            }
        }
        else
        {
            if (inFlame == true)
            {
                StartCoroutine(Propagate());
            }  
        }

    }
    
    public void StopFire()
    {
        Destroy(fireInstance);
        GetComponent<Renderer>().material.color = endColor;
        hasBeenInFlame = true;
    }
    
}
