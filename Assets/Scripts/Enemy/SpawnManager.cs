using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject gameManager;
    [SerializeField] GameObject spawner;

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
    }
}
