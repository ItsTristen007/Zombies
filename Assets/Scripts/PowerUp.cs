using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float timer = 20f;

    void Awake()
    {
        StartCoroutine(Disappear());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
