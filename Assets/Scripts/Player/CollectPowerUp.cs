using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectPowerUp : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] TextMeshProUGUI powerUpText;
    PlayerWeapon weapon;
    float normalDamage;
    float maxAmmo;
    float normalPoints;

    GameObject[] copierEnemies;
    GameObject[] shredderEnemies;

    bool instakill;
    bool infiniteAmmo;
    bool doublePoints;
    bool nuke;
    bool powerUpActive;

    float timer = 0f;
    float powerUpTimer = 15f;

    public bool GetPowerUpActive()
    {
        return powerUpActive;
    }

    public void SetPowerUpActive(bool newState)
    {
        powerUpActive = newState;
    }

    void Awake()
    {
        powerUpText.text = "";
        weapon = GetComponent<PlayerWeapon>();
        normalDamage = weapon.GetBulletDamage();
        maxAmmo = weapon.GetMaxAmmo();
        normalPoints = enemy.GetComponent<EnemyHealth>().GetPoints();

        instakill = false;
        infiniteAmmo = false;
        doublePoints = false;
        nuke = false;
        powerUpActive = false;
        timer = powerUpTimer;
    }

    void Update()
    {
        if (instakill)
        {
            normalDamage = weapon.GetBulletDamage();
            timer = timer - Time.deltaTime;
            powerUpText.text = string.Format("Stapler ({0:#.})", timer);
        }
        else if (infiniteAmmo)
        {
            maxAmmo = weapon.GetMaxAmmo();
            timer = timer - Time.deltaTime;
            weapon.SetCurrentAmmo(maxAmmo);
            powerUpText.text = string.Format("Paper Stack ({0:#.})", timer);
        }
        else if (doublePoints)
        {
            timer = timer - Time.deltaTime;
            powerUpText.text = string.Format("Photocopier ({0:#.})", timer);
        }
        else if (nuke)
        {
            powerUpText.text = "Paper Shredder!";
        }
        else
        {
            timer = powerUpTimer;
            powerUpText.text = "";
        }
    }

    void Stapler()
    {
        powerUpActive = true;
        weapon.SetBulletDamage(normalDamage * 10f);
        StartCoroutine(StaplerTime());
    }

    void PaperStack()
    {
        powerUpActive = true;
        StartCoroutine(PaperStackTime());
    }

    void Photocopier()
    {
        powerUpActive = true;
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
        powerUpActive = true;
        shredderEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in shredderEnemies)
        {
            Destroy(enemy);
        }
        StartCoroutine(PaperShredderTime());
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
        instakill = true;
        yield return new WaitForSeconds(powerUpTimer);
        weapon.SetBulletDamage(normalDamage);
        instakill = false;
        powerUpActive = false;
    }

    IEnumerator PaperStackTime()
    {
        infiniteAmmo = true;
        yield return new WaitForSeconds(powerUpTimer);
        infiniteAmmo = false;
        powerUpActive = false;
    }

    IEnumerator PhotocopierTime()
    {
        doublePoints = true;
        yield return new WaitForSeconds(powerUpTimer);
        copierEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy.GetComponent<EnemyHealth>().SetPoints(normalPoints);
        foreach (var enemy in copierEnemies)
        {
            enemy.GetComponent<EnemyHealth>().SetPoints(normalPoints);
        }
        doublePoints = false;
        powerUpActive = false;
    }

    IEnumerator PaperShredderTime()
    {
        nuke = true;
        yield return new WaitForSeconds(3f);
        GetComponent<PlayerScore>().ChangeScore(50);
        nuke = false;
        powerUpActive = false;
    }
}
