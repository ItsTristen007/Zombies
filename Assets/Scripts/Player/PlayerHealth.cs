using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float maxHealth = 100;
    float currentHealth;
    [SerializeField] float timeBeforeRegen = 2.5f;
    [SerializeField] float invulnerableTime = 0.5f;
    bool isInvulnerable;

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isInvulnerable)
            {
                ChangeHealth(-collision.gameObject.GetComponent<EnemyHealth>().GetDamage());
                StartCoroutine(InvulnerableTime());
                StartCoroutine(HealWaitTime());
            }
        }
    }

    IEnumerator InvulnerableTime()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    IEnumerator HealWaitTime()
    {
        float tempHealth = currentHealth;
        yield return new WaitForSeconds(timeBeforeRegen);
        if (tempHealth == currentHealth)
        {
            ChangeHealth(maxHealth);
        }
    }

}
