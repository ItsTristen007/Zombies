using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator animator;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float damage = 25f;
    [SerializeField] float attackRange;
    bool playerInAttackRange;
    bool hasAttacked;

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        agent.speed = moveSpeed;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        
        if (GetComponent<Transform>().rotation.z > 0 && GetComponent<EnemyHealth>().GetCurrentHealth() <= 0)
        {
            agent.isStopped = true;
            animator.SetTrigger("DIE");
            hasAttacked = true;
            agent.SetDestination(transform.position);
        }
        else if (!playerInAttackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        transform.rotation.SetLookRotation(player.transform.position, Vector3.up);
    }

    void Attack()
    {
        agent.isStopped = true;
        transform.rotation.SetLookRotation(player.transform.position, Vector3.up);

        if (!hasAttacked && !player.GetComponent<PlayerHealth>().GetIsInvulnerable())
        {
            player.GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hasAttacked = true;
            StartCoroutine(ResetAttackTime());
        }
    }

    IEnumerator ResetAttackTime()
    {
        yield return new WaitForSeconds(player.GetComponent<PlayerHealth>().GetInvulnerableTime());
        if (GetComponent<EnemyHealth>().GetCurrentHealth() > 0)
        {
            hasAttacked = false;
        }
    }

}
