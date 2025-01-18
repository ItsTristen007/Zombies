using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject newWaveText;
    [SerializeField] float timeBeforeSpawn = 1.5f;
    [SerializeField] float timeBetweenWaves = 8f;
    [SerializeField] int waveSpawnNumber;
    GameObject[] enemies;
    int numSpawned;
    bool isSpawning = true;
    bool isWaiting;
    bool waveStarting;

    public bool GetWaveStarting()
    {
        return waveStarting;
    }

    public void SetWaveStarting(bool newState)
    {
        waveStarting = newState;
    }

    void Awake()
    {
        newWaveText.SetActive(false);
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
            if (enemies.Length == 0 && !isWaiting)
            {
                StartCoroutine(TimeUntilNewWave());
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
        newWaveText.SetActive(true);
        yield return new WaitForSeconds(timeBetweenWaves);
        numSpawned = 0;
        if (enemy.name == "CommonEnemy")
        {
            waveSpawnNumber++;
        }
        isSpawning = true;
        isWaiting = false;
        waveStarting = true;
        newWaveText.SetActive(false);
    }
}
