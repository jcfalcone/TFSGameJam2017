using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Detection))]
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : BaseCharacter
{
    private const float MAX_HEALTH = 1;

    [SerializeField]
    private GameObject _shot;
    [SerializeField]
    private float _shotInterval;
    private Patrol _patrol;
    public bool seeingPlayer = false;

    [SerializeField]
    private Waypoint[] path;

    [SerializeField]
    [Range(0f, 10f)]
    float minRangePlayer;

    [SerializeField]
    [Range(0, 360)]
    float minAnglePlayer;

    [SerializeField]
    [Range(0, 360)]
    float maxAnglePlayer;

    Transform player;

    void Awake()
    {
        // setup initial definitions
        SetStamina(0);
        SetMaxHealth(MAX_HEALTH);
        SetHealth(MAX_HEALTH);
        SetupRigidbody();

        _patrol = GetComponent<Patrol>();

        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");

        if (tempPlayer)
        {
            this.player = tempPlayer.transform;
        }
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

        // TODO replace with proper Detection and stateManagement once available
        if (seeingPlayer)
        {
            _patrol.StopPatrolling();
        }
        else
        {
            _patrol.PatrolAround();
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

    public void Die()
    {
        // TODO add animation, SFX, etc

        // have the EnemySpawner determine how to Respawn the Enemy
        EnemySpawner.instance.Respawn(gameObject);

        // reset the Enemy info
        SetHealth(MAX_HEALTH);
        isDead = false;
        // TODO include other data, such as state
        // TODO (if needed) CancelInvoke of Shoot
    }

    public Waypoint[] GetPath()
    {
        return this.path;
    }

    public Transform GetPlayer()
    {
        return this.player;
    }

    public float GetMinRange()
    {
        return this.minRangePlayer;
    }

    public float GetMinAngle()
    {
        return this.minAnglePlayer;
    }

    public float GetMaxAngle()
    {
        return this.maxAnglePlayer;
    }
}
