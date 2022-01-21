using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public bool inFlame;
    public GameObject firePrefab;
    private GameObject fireInstance;
    public Vector3 decal;
    public float range = 3;
    public float propagateDelay = 3f;
    
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool repeatable = false;
    float startTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    public void Ignite()
    {
        if (inFlame == false)
        {
            inFlame = true;
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
                    hit.collider.gameObject.GetComponent<Flamable>().Ignite();
                }
            }
        }

        yield return new WaitForSeconds(propagateDelay);
        
        if (inFlame == true)
        {
            StartCoroutine(Propagate());
        }
    }

    void Update()
    {
        if (inFlame)
        {
            if (!repeatable)
            {
                float t = (Time.time - startTime) * speed;
                GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
            }
            else
            {
                float t = (Mathf.Sin(Time.time - startTime) * speed);
                GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
            }
        }
    }
}
