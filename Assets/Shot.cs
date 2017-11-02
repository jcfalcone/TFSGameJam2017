using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.TakeDamage(enemy.GetMaxHealth());
            enemy.Die();
        }
    }
}
