using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject material;
    bool startedParticles;

    void Awake()
    {
        startedParticles = false;
    }

    void Update()
    {
        if (GetComponent<EnemyHealth>().GetCurrentHealth() <= 0)
        {
            if (!particles.isPlaying && !startedParticles)
            {
                particles.Play();
                startedParticles = true;
                material.GetComponent<Renderer>().material.SetColor("_Main_Colour", Color.gray);
                Destroy(gameObject, particles.main.duration);
            }
        }
    }
}
