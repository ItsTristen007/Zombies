using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float timeBeforeSpawn = 1.5f;
    [SerializeField] int waveSpawnNumber;
    int numSpawned;
    bool isSpawning = true;
    bool isWaiting;

    void Update()
    {
        if (isSpawning && numSpawned < waveSpawnNumber)
        {
            Instantiate(enemy, transform.position + Vector3.up, Quaternion.identity);
            numSpawned++;
            isSpawning = false;
        }
        else if (!isSpawning)
        {
            if (!isWaiting)
            {
                StartCoroutine(SpawnWaitTime());
            }
        }
    }

    IEnumerator SpawnWaitTime()
    {
        isWaiting = true;
        yield return new WaitForSeconds(timeBeforeSpawn);
        isSpawning = true;
        isWaiting = false;
    }
}
