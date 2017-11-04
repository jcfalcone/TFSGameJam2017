using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanterExplosion : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;

    void OnCollisionEnter(Collision c)
    {
        explosion.Play();
        Destroy(gameObject, 0.5f);
    }
}
