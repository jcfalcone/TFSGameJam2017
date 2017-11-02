using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected float maxHealth;
    protected float health;

    protected float stamina;

    protected Rigidbody rb;
    protected Animator anim;

    protected bool isDead;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            isDead = true;
    }

    /* SET */
    public void SetStamina( float newStamina ) { this.stamina = newStamina; }

    public void SetMaxHealth( float newMaxHealth ) { this.maxHealth = newMaxHealth; }
    public void SetHealth( float newHealth ) { this.health = newHealth; }

    public void SetupRigidbody() { rb = GetComponent<Rigidbody>(); }

    /* GET */
    public float GetStamina() { return this.stamina; }

    public float GetMaxHealth() { return this.maxHealth; }
    public float GetHealth() { return this.health; }
    public float GetRationHealth() { return (this.health / this.maxHealth) * 100; }

    public Rigidbody GetRigidbody() { return this.rb; }
}
