using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Detection))]

public class Enemy : BaseCharacter
{
    [SerializeField]
    private GameObject _shot;
    [SerializeField]
    private float _shotInterval;

    void Awake()
    {
        // setup initial definitions
        SetStamina(0);
        SetMaxHealth(1);
        SetupRigidbody();
    }

    void Update()
    {
        // TODO replace with proper stateManagement triggers once available
        if (Input.GetKeyDown(KeyCode.K))
        {
            // shoot immediately, and then again after a certain time
            InvokeRepeating("Shoot", 0, _shotInterval);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CancelInvoke("Shoot");
        }
    }

    void Shoot()
    {
        // generate and reparent shot
        GameObject shot = Instantiate(_shot, transform.position + transform.forward, transform.rotation);
        shot.transform.parent = transform;

        // TODO	remove once proper shot destruction is in place
        Destroy(shot, 1);
    }
}
