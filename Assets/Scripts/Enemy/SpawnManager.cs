using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject gameManager;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject bruteSpawner;
    EnemyHealth enemyStats;
    bool healthIncreased;
    bool damageIncreased;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        enemyStats = enemy.GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (spawner.GetComponent<Spawner>().GetWaveStarting())
        {
            gameManager.GetComponent<GameManager>().UpdateWave();
            spawner.GetComponent<Spawner>().SetWaveStarting(false);
            healthIncreased = false;
            damageIncreased = false;
            // Brute enemies will only spawn every 5 rounds
            if (gameManager.GetComponent<GameManager>().GetCurrentWave() % 5 == 0)
            {
                bruteSpawner.SetActive(true);
            }
            else
            {
                bruteSpawner.SetActive(false);
            }
        }

        if (gameManager.GetComponent<GameManager>().GetCurrentWave() % 5 == 0 && !healthIncreased)
        {
            enemyStats.SetMaxHealth(enemyStats.GetMaxHealth() + 5);
            healthIncreased = true;
        }
        if (gameManager.GetComponent<GameManager>().GetCurrentWave() % 10 == 0 && !damageIncreased)
        {
            enemyStats.SetDamage(enemyStats.GetDamage() * 2);
            damageIncreased = true;
        }
    }
}
