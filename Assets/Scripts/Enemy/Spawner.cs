using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    GameObject gameManager;
    [SerializeField] float timeBeforeSpawn = 1.5f;
    [SerializeField] float timeBetweenWaves = 8f;
    [SerializeField] int waveSpawnNumber;
    GameObject[] enemies;
    int numSpawned;
    bool isSpawning = true;
    bool isWaiting;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

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
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                if (!isWaiting)
                {
                    StartCoroutine(TimeUntilNewWave());
                }
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

    IEnumerator TimeUntilNewWave()
    {
        isWaiting = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        numSpawned = 0;
        gameManager.GetComponent<GameManager>().UpdateWave();
        isWaiting = false;
    }
}
