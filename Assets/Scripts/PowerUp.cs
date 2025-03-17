using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    AudioSource source;
    public AudioClip pickUpSound;
    float timer = 20f;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Disappear());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            source.PlayOneShot(pickUpSound);
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<Renderer>().enabled = false;
            }
            Destroy(gameObject, 4f);
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
