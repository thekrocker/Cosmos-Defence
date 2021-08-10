using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfig waveConfig;
    List<Transform> waypoints;
    

    int waypointIndex = 0;


    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    
    void Update()
    {
        EnemyMove();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;

    }

    private void EnemyMove()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);  // (current, target, speed) ardından pozisyon target pozisyonuna geldiğinde sayıyı arttırdık.

            if (transform.position == targetPosition)
            {

                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
