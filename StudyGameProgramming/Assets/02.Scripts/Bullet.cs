using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float health;
    public float damage;
    public string firedObject;
    
    void Start()
    {
        Destroy(gameObject, health);
    }
}
