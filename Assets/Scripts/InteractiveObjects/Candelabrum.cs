using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candelabrum : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject rope;

    void OnParticleCollision(GameObject other)
    {
        if(other.name == "Projectile")
        {
            rope.AddComponent<Rigidbody>();
            rope.transform.parent = null;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
