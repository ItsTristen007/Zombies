using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerUp : MonoBehaviour
{
    PlayerWeapon weapon;
    float normalDamage;

    float powerUpTimer = 15f;

    void Awake()
    {
        weapon = GetComponent<PlayerWeapon>();
        normalDamage = weapon.GetBulletDamage();
    }

    void Update()
    {
        
    }

    void Stapler()
    {
        weapon.SetBulletDamage(normalDamage * 5f);
        StartCoroutine(StaplerTime());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Instakill"))
        {
            Stapler();
        }
    }

    IEnumerator StaplerTime()
    {
        yield return new WaitForSeconds(powerUpTimer);
        weapon.SetBulletDamage(normalDamage);
    }
}
