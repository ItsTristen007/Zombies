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
    float tempHealth;
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

    public float GetInvulnerableTime()
    {
        return invulnerableTime;
    }

    public bool GetIsInvulnerable()
    {
        return isInvulnerable;
    }

    void Awake()
    {
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        currentHealth = maxHealth;
        tempHealth = currentHealth;
    }

    void Update()
    {
        if (!isInvulnerable)
        {
            if (currentHealth < tempHealth)
            {
                StartCoroutine(InvulnerableTime());
                StartCoroutine(CameraShake());
            }
            if (currentHealth != tempHealth)
            {
                StartCoroutine(HealWaitTime());
            }
            tempHealth = currentHealth;
        }
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
