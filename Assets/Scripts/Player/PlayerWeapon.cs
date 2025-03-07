using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float maxAmmo = 10f;
    float currentAmmo;
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] float bulletDamage = 15f;
    [SerializeField] float bulletLifetime = 3f;
    [SerializeField] float fireRate = 0.8f;
    [SerializeField] float reloadSpeed = 1.5f;
    bool usingPistol;
    bool usingShotgun;
    bool usingSMG;

    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject smg;

    public float GetMaxAmmo()
    {
        return maxAmmo;
    }

    public float GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void SetCurrentAmmo(float newAmmo)
    {
        currentAmmo = newAmmo;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public float GetBulletDamage()
    {
        return bulletDamage;
    }

    public void SetBulletDamage(float newDamage)
    {
        bulletDamage = newDamage;
    }

    public float GetBulletLifetime()
    {
        return bulletLifetime;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetReloadSpeed()
    {
        return reloadSpeed;
    }

    public bool GetUsingPistol()
    {
        return usingPistol;
    }

    public bool GetUsingShotgun()
    {
        return usingShotgun;
    }

    public bool GetUsingSMG()
    {
        return usingSMG;
    }

    void Awake()
    {
        currentAmmo = maxAmmo;
        usingPistol = true;
        usingShotgun = false;
        usingSMG = false;

        pistol.SetActive(true);
        shotgun.SetActive(false);
        smg.SetActive(false);
    }

    public void SwitchToPistol()
    {
        bulletDamage = 15f;
        bulletLifetime = 3f;
        maxAmmo = 10f;
        fireRate = 0.8f;
        reloadSpeed = 1.5f;

        currentAmmo = maxAmmo;
        usingPistol = true;
        usingShotgun = false;
        usingSMG = false;

        pistol.SetActive(true);
        shotgun.SetActive(false);
        smg.SetActive(false);
    }

    public void SwitchToShotgun()
    {
        bulletDamage = 50f;
        bulletLifetime = 0.3f;
        maxAmmo = 4f;
        fireRate = 2f;
        reloadSpeed = 2.5f;

        currentAmmo = maxAmmo;
        usingPistol = false;
        usingShotgun = true;
        usingSMG = false;

        pistol.SetActive(false);
        shotgun.SetActive(true);
        smg.SetActive(false);
    }

    public void SwitchToSMG()
    {
        bulletDamage = 5f;
        bulletLifetime = 2.5f;
        maxAmmo = 30f;
        fireRate = 0.1f;
        reloadSpeed = 1f;

        currentAmmo = maxAmmo;
        usingPistol = false;
        usingShotgun = false;
        usingSMG = true;

        pistol.SetActive(false);
        shotgun.SetActive(false);
        smg.SetActive(true);
    }
}
