using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    PlayerWeapon weapon;

    void Awake()
    {
        weapon = GameObject.FindWithTag("Player").GetComponent<PlayerWeapon>();
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(weapon.GetBulletLifetime());
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
