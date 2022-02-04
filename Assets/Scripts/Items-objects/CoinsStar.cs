using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsStar : MonoBehaviour
{

    void OnCollisionEnter(Collider other)
    {
        //tu peux tester de différentes manieres

        // 1- Soit l'objet avec lequel la collision s'effectue est taggé, dans ce cas tu peux utiliser son tag comme condition :
        if (other.gameObject.tag == "Coins")
        {
            Destroy(gameObject); // attention: gameObject et pas GameObject
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

