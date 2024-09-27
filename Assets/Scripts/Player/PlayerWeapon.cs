using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float maxAmmo1 = 10f;
    [SerializeField] float maxAmmo2 = 10f;
    float currentAmmo1;
    float currentAmmo2;
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] float bulletDamage = 1f;

    bool usingWeapon1;
    bool usingWeapon2;

    public float GetMaxAmmo1()
    {
        return maxAmmo1;
    }

    public float GetMaxAmmo2()
    {
        return maxAmmo2;
    }

    public float GetCurrentAmmo1()
    {
        return currentAmmo1;
    }

    public void SetCurrentAmmo1(float newAmmo)
    {
        currentAmmo1 = newAmmo;
    }

    public float GetCurrentAmmo2()
    {
        return currentAmmo2;
    }

    public void SetCurrentAmmo2(float newAmmo2)
    {
        currentAmmo2 = newAmmo2;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public float GetBulletDamage()
    {
        return bulletDamage;
    }

    public bool UsingWeapon1()
    {
        return usingWeapon1;
    }

    public void SetWeapon1(bool newState)
    {
        usingWeapon1 = newState;
    }

    public bool UsingWeapon2()
    {
        return usingWeapon2;
    }

    public void SetWeapon2(bool newState)
    {
        usingWeapon2 = newState;
    }

    void Awake()
    {
        currentAmmo1 = maxAmmo1;
        usingWeapon1 = true;
        usingWeapon2 = false;
    }
}
