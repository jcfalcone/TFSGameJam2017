using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private Transform _initialSpawnSpots;

    [SerializeField]
    private Transform _relocationSpots;

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

    public void Respawn(GameObject enemy)
    {
        // get a new location
        Transform newSpot = DetermineNewLocation();

        // if one was found, reposition and reparent the enemy
        if (newSpot)
        {
            enemy.transform.position = newSpot.position;
            enemy.transform.rotation = newSpot.rotation;
            enemy.transform.parent = newSpot;
        }
    }

    public void Basic()
    { }

    Transform DetermineNewLocation()
    {
        // select a random spot amongst the relocation options
        int spot = Random.Range(0, _relocationSpots.childCount);

        // check if there's an enemy there already
        // if there isn't, return that location
        if (_relocationSpots.GetChild(spot).childCount <= 0)
        {
            return _relocationSpots.GetChild(spot);
        }
        // if there is, try again
        else
        {
            DetermineNewLocation();
        }

        // if getting here, all locations are occupied (which shouldn't happen) and we return null
        Debug.LogError("Couldn't resolve new enemy location");
        return null;
    }
}
