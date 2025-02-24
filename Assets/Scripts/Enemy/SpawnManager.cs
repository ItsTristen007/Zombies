using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject gameManager;
    [SerializeField] GameObject commonEnemy;
    [SerializeField] GameObject bruteEnemy;
    [SerializeField] GameObject crawlerEnemy;

    [SerializeField] GameObject newWaveText;
    [SerializeField] float timeBeforeSpawn = 1.5f;
    [SerializeField] float timeBetweenWaves = 8f;
    [SerializeField] int waveSpawnNumber;
    GameObject[] enemies;
    GameObject[] spawners;
    int numSpawned;
    bool isSpawning = true;
    bool isWaiting;
    bool waveStarting;

    bool healthIncreased;
    bool damageIncreased;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        newWaveText.SetActive(false);
    }

    void Update()
    {
        if (isSpawning && numSpawned < waveSpawnNumber)
        {
            numSpawned++;
            isSpawning = false;
            spawners = GameObject.FindGameObjectsWithTag("Spawner");

            foreach (GameObject s in spawners)
            {
                s.GetComponent<Spawner>().SpawnEnemy("Common");
            }
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

        if (waveStarting)
        {
            gameManager.GetComponent<GameManager>().UpdateWave();
            waveStarting = false;
            healthIncreased = false;
            damageIncreased = false;
        }

        if (gameManager.GetComponent<GameManager>().GetCurrentWave() % 5 == 0 && !healthIncreased)
        {
            commonEnemy.GetComponent<EnemyHealth>().SetMaxHealth(commonEnemy.GetComponent<EnemyHealth>().GetMaxHealth() + 10);
            healthIncreased = true;
        }
        if (gameManager.GetComponent<GameManager>().GetCurrentWave() % 10 == 0 && !damageIncreased)
        {
            commonEnemy.GetComponent<EnemyController>().SetDamage(commonEnemy.GetComponent<EnemyController>().GetDamage() * 2);
            damageIncreased = true;
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
        waveSpawnNumber++;
        isSpawning = true;
        isWaiting = false;
        waveStarting = true;
        newWaveText.SetActive(false);
    }
}
