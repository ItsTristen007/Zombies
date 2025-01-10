using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] powerUpPrefab;
    [SerializeField] float maxHealth = 5f;
    float currentHealth;
    [SerializeField] float damage = 25f;
    [SerializeField] float points = 10f;
    bool pointsGiven;

    int powerUpDropChance;
    int powerUpType;
    bool chanceCalculated;

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public float GetPoints()
    {
        return points;
    }

    public void SetPoints(float newPoints)
    {
        points = newPoints;
    }

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
        pointsGiven = false;
        chanceCalculated = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            transform.Rotate(-90, 0, 0);
            transform.position -= new Vector3(0, 0.5f, 0);

            GetComponent<Collider>().enabled = false;
            
            Invoke("Die", 0.5f);
        }
    }

    void Die()
    {
        var randomNum = -1;
        if (!chanceCalculated)
        {
            powerUpDropChance = Random.Range(0, 21);
            randomNum = Random.Range(0, 21);
            chanceCalculated = true;
        }

        if (powerUpDropChance == randomNum && !player.GetComponent<CollectPowerUp>().GetPowerUpActive())
        {
            player.GetComponent<CollectPowerUp>().SetPowerUpActive(true);
            powerUpType = Random.Range(0, 4);
            Instantiate(powerUpPrefab[powerUpType], transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        if (!pointsGiven)
        {
            player.GetComponent<PlayerScore>().ChangeScore(points);
            pointsGiven = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            currentHealth -= player.GetComponent<PlayerWeapon>().GetBulletDamage();
        }
    }

   

}
