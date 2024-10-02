using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
        if(GetComponent<Transform>().rotation.z >0)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }

}
