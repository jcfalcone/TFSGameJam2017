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
        foreach (Transform spot in _initialSpawnSpots)
        {
            GameObject.Instantiate(_enemyPrefab, spot.position, spot.rotation);
        }
    }
}
