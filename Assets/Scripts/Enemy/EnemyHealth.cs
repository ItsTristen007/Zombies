using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float maxHealth = 5f;
    float currentHealth;
    [SerializeField] float damage = 25f;
    [SerializeField] float points = 10f;

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
