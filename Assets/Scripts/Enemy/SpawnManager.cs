using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject gameManager;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject enemy;
    bool healthIncreased;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (spawner.GetComponent<Spawner>().GetWaveStarting())
        {
            gameManager.GetComponent<GameManager>().UpdateWave();
            spawner.GetComponent<Spawner>().SetWaveStarting(false);
        }

        if (gameManager.GetComponent<GameManager>().GetCurrentWave() % 5 == 0 && !healthIncreased)
        {
            enemy.GetComponent<EnemyHealth>().SetMaxHealth(enemy.GetComponent<EnemyHealth>().GetMaxHealth() + 1);
            healthIncreased = true;
        }
    }
}
