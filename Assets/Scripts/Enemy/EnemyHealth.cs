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

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
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
       
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            this.GetComponent<Transform>().Rotate(0, 0, 90);
            
            Invoke("Die", (float)0.15);
        }
    }

    void Die()
    {
        powerUpDropChance = Random.Range(0, 30);
        if (powerUpDropChance == 15 && !player.GetComponent<CollectPowerUp>().GetPowerUpActive())
        {
            player.GetComponent<CollectPowerUp>().SetPowerUpActive(true);
            powerUpType = Random.Range(0, 4);
            Instantiate(powerUpPrefab[powerUpType], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
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
