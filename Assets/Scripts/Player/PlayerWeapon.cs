using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float maxAmmo = 30f;
    float currentAmmo;
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] float bulletDamage = 5f;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] float reloadSpeed = 1f;

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

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetReloadSpeed()
    {
        return reloadSpeed;
    }

    void Awake()
    {
        currentAmmo = maxAmmo;
    }

    public void SwitchToPistol()
    {
        bulletDamage = 10f;
        maxAmmo = 10f;
        fireRate = 0.8f;
        reloadSpeed = 1.5f;

        currentAmmo = maxAmmo;
    }

    public void SwitchToShotgun()
    {
        bulletDamage = 40f;
        maxAmmo = 4f;
        fireRate = 3f;
        reloadSpeed = 2.5f;

        currentAmmo = maxAmmo;
    }

    public void SwitchToSMG()
    {
        bulletDamage = 5f;
        maxAmmo = 30f;
        fireRate = 0.1f;
        reloadSpeed = 1f;

        currentAmmo = maxAmmo;
    }
}
