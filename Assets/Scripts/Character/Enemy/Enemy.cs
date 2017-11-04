using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof(Collider))]
// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(Animator))]
// [RequireComponent(typeof(Patrol))]
// [RequireComponent(typeof(Detection))]
// [RequireComponent(typeof(NavMeshAgent))]

public class Enemy : BaseCharacter
{
    private const float MAX_HEALTH = 1;

    [SerializeField]
    private float _shotInterval;

    [SerializeField]
    private Waypoint[] path;

    [SerializeField]
    private Spawnpoint[] spawnpoint;

    [SerializeField]
    [Range(0f, 10f)]
    float minRangePlayer;

    [SerializeField]
    [Range(0, 360)]
    float minAnglePlayer;

    [SerializeField]
    [Range(0, 360)]
    float maxAnglePlayer;

    [Header("Shoot")]
    [SerializeField]
    private LayerMask hitLayers;

    [SerializeField]
    private Transform bulletSpawn;

    [SerializeField]
    private GameObject bulletPrefab;

    [Header("State Manager")]
    [SerializeField]
    EnemyStateManager stateManager;

    Transform player;

    public bool die;

    void Awake()
    {
        this.Init();

        SetupRigidbody();

        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");

        if (tempPlayer)
        {
            this.player = tempPlayer.transform;
        }

        this.stateManager.Init(this);
        this.stateManager.SetState(EnemyStateTemplate.StatesAI.Idle);
        //StartCoroutine(KillTest());
    }

    void Update()
    {
        this.stateManager.Tick();

        if (die)
        {
            KillTest();
            die = false;
        }
    }

    public void Init()
    {

        SetStamina(0);
        SetMaxHealth(MAX_HEALTH);
        SetHealth(MAX_HEALTH);
    }

    public Waypoint[] GetPath()
    {
        return this.path;
    }

    public Spawnpoint[] GetSpawnpoint()
    {
        return this.spawnpoint;
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

    public Transform GetBulletSpawn()
    {
        return this.bulletSpawn;
    }

    public GameObject GetBulletPrefab()
    {
        return this.bulletPrefab;
    }

    public LayerMask GetHitLayer()
    {
        return this.hitLayers;
    }


    void KillTest()
    {
        this.health = 0;
    }
}
