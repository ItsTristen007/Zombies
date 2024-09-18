using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float maxAmmo = 10f;
    float currentAmmo;
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] float bulletDamage = 1f;

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

    void Awake()
    {
        currentAmmo = maxAmmo;
    }
}
