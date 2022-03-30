using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float hp;
    
    void Start()
    {
        hp = 10;
        Destroy(gameObject, hp);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
            Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
