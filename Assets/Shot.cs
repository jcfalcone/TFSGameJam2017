using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            BaseCharacter character = other.GetComponent<BaseCharacter>();
            character.TakeDamage(character.GetMaxHealth());
        }
    }
}
