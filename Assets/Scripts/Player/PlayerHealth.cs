using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    float currentHealth;
    [SerializeField] float regenTime = 2.5f;

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

    IEnumerator HealthRegen()
    {
        yield return new WaitForSeconds(regenTime);
    }

}
