using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerUp : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    PlayerWeapon weapon;
    float normalDamage;
    float maxAmmo;
    float normalPoints;

    GameObject[] copierEnemies;
    GameObject[] shredderEnemies;

    bool infiniteAmmo;

    float powerUpTimer = 15f;

    void Awake()
    {
        weapon = GetComponent<PlayerWeapon>();
        normalDamage = weapon.GetBulletDamage();
        maxAmmo = weapon.GetMaxAmmo();
        normalPoints = enemy.GetComponent<EnemyHealth>().GetPoints();
        infiniteAmmo = false;
    }

    void Update()
    {
        if (infiniteAmmo)
        {
            weapon.SetCurrentAmmo(maxAmmo);
        }
    }

    void Stapler()
    {
        weapon.SetBulletDamage(normalDamage * 5f);
        StartCoroutine(StaplerTime());
    }

    void PaperStack()
    {
        StartCoroutine(PaperStackTime());
    }

    void Photocopier()
    {
        copierEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy.GetComponent<EnemyHealth>().SetPoints(normalPoints * 2f);
        foreach (var enemy in copierEnemies)
        {
            enemy.GetComponent<EnemyHealth>().SetPoints(normalPoints * 2f);
        }
        StartCoroutine(PhotocopierTime());
    }

    void PaperShredder()
    {
        shredderEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in shredderEnemies)
        {
            Destroy(enemy);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Instakill"))
        {
            Stapler();
        }
        else if (other.gameObject.CompareTag("InfiniteAmmo"))
        {
            PaperStack();
        }
        else if (other.gameObject.CompareTag("DoublePoints"))
        {
            Photocopier();
        }
        else if (other.gameObject.CompareTag("Nuke"))
        {
            PaperShredder();
        }
    }

    IEnumerator StaplerTime()
    {
        yield return new WaitForSeconds(powerUpTimer);
        weapon.SetBulletDamage(normalDamage);
    }

    IEnumerator PaperStackTime()
    {
        infiniteAmmo = true;
        yield return new WaitForSeconds(powerUpTimer);
        infiniteAmmo = false;
    }

    IEnumerator PhotocopierTime()
    {
        yield return new WaitForSeconds(powerUpTimer);
        copierEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy.GetComponent<EnemyHealth>().SetPoints(normalPoints);
        foreach (var enemy in copierEnemies)
        {
            enemy.GetComponent<EnemyHealth>().SetPoints(normalPoints);
        }
    }
}
