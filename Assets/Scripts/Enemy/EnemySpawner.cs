using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private Transform[] _initialSpawnSpots;

    [SerializeField]
    private Transform[] _relocationSpots;

    void Start()
    {
        SpawnLevelEnemies();
    }

    void SpawnLevelEnemies()
    {
        // generate a new enemy on each initial location
        foreach (Transform spot in _initialSpawnSpots)
        {
            GameObject.Instantiate(_enemyPrefab, spot);
        }
    }

    void Update()
    {
        // TODO replace with proper death trigger based of what is sent from specific enemies as they die
        if (Input.GetKeyDown(KeyCode.D))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // get a new location
        Transform newSpot = DetermineNewLocation();

        // if one was found, reposition and reparent the enemy
        if (newSpot)
        {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            transform.parent = newSpot;
        }
    }

    Transform DetermineNewLocation()
    {
        // select a random spot amongst the relocation options
        int spot = Random.Range(0, _relocationSpots.Length);

        // check if there's an enemy there already
        // if there isn't, return that location
        if (_relocationSpots[spot].childCount <= 0)
        {
            return _relocationSpots[spot];
        }
        // if there is, try again
        else
        {
            DetermineNewLocation();
        }

        // if getting here, all locations are occupied (which shouldn't happen) and we return null
        Debug.LogError("Couldn'r resolve new enemy location");
        return null;
    }
}
