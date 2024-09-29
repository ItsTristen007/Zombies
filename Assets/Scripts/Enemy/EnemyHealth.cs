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

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        powerUpDropChance = Random.Range(0, 20);
        if (powerUpDropChance == 10)
        {
            powerUpType = Random.Range(0, 4);
            Instantiate(powerUpPrefab[powerUpType], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        player.GetComponent<PlayerScore>().ChangeScore(points);
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
