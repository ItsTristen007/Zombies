using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] GameObject enemy;
    [SerializeField] float maxHealth = 100;
    float currentHealth;
    [SerializeField] float timeBeforeRegen = 2.5f;
    [SerializeField] float invulnerableTime = 0.5f;
    bool isInvulnerable;
    float shakeTime = 0.15f;

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
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
                StartCoroutine(CameraShake());
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

    IEnumerator CameraShake()
    {
        noise.m_AmplitudeGain = 4;
        noise.m_FrequencyGain = 4;
        yield return new WaitForSeconds(shakeTime);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }

}
