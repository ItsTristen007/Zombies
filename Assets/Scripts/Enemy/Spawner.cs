using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject commonEnemy;
    [SerializeField] GameObject bruteEnemy;
    [SerializeField] GameObject crawlerEnemy;

    public void SpawnEnemy(string enemyType)
    {
        if (enemyType == "Common")
        {
            Instantiate(commonEnemy, transform.position + Vector3.up, Quaternion.identity);
        }
        else if (enemyType == "Brute")
        {
            Instantiate(bruteEnemy, transform.position + Vector3.up, Quaternion.identity);
        }
        else if (enemyType == "Crawler")
        {
            Instantiate(crawlerEnemy, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
