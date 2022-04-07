using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitProcessing : MonoBehaviour
{
    public GameObject topParent;
    public float DamageRatio;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Bullet bulletStats = other.GetComponent<Bullet>();

            if (topParent.tag != bulletStats.firedObject)
                topParent.GetComponent<Health>().getHealth(-1.0f * bulletStats.damage * DamageRatio);

            Destroy(other.gameObject);
        }
    }
}
