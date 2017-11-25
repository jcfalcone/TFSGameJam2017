using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanterExplosion : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;

    void OnCollisionEnter(Collision c)
    {
        explosion.transform.parent = null;
        explosion.Play();
        Destroy(explosion, 3f);
        Destroy(gameObject, 0.5f);
    }
}
