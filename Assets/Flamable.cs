using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public bool inFlame;
    public GameObject firePrefab;
    public GameObject fireInstance;
    public Vector3 decal;
    public float range = 3;
    public float propagateDelay = 3f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Ignite()
    {
        if (inFlame == false)
        {
            inFlame = true;
            fireInstance = Instantiate(firePrefab, transform.position + decal, Quaternion.identity);
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
