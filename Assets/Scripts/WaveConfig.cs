using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float randomSpawnFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;


    public GameObject GetEnemyPrefab() { return enemyPrefab;}
    
    public List<Transform> GetWaypoints() {

        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform) // For each child in pathPrefab getting the transform info
        {
            waveWaypoints.Add(child); // add the transform of that child into our waveWaypoints List.
        } 


        return waveWaypoints; 
    
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetRandomSpawnFactor() { return randomSpawnFactor; }
    public float GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }


}
