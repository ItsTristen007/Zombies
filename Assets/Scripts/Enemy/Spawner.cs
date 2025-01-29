using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    public void SpawnEnemy()
    {
        Instantiate(enemy, transform.position + Vector3.up, Quaternion.identity);
    }
}
