using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    float timer = 3f;

    void Awake()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Environment") || other.gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
