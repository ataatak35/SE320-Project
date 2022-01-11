using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour{
    public float enemyHealth = 100f;
    public float attackDamage = 10f;
    public bool isDead;
    public float pursueDistance = 30f;
    public float attackDistance = 2.5f;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private MeshCollider meshCollider;
    private Rigidbody rigidbody;
    

    void Start(){
        isDead = false;
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        meshCollider = gameObject.transform.GetChild(1).GetComponent<MeshCollider>();
        rigidbody = gameObject.transform.GetChild(1).GetComponent<Rigidbody>();
        
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (enemyHealth <= 0){
            Die();
        }

        if (!isDead){
            GoToPlayer();
            Attack();    
        }
        
    }

    public void GetDamage(float damage){
        enemyHealth -= damage;
        if (enemyHealth <= 0){
            Die();
        }
    }

    public void Die(){
        navMeshAgent.isStopped = true;
        isDead = true;
        animator.SetBool("isDead", true);
        meshCollider.enabled = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(gameObject, 10f);
    }

    public void Attack(){
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackDistance){
            navMeshAgent.isStopped = true;
            animator.SetBool("isAttacking", true);
        }
        else{
            navMeshAgent.isStopped = false;
            animator.SetBool("isAttacking", false);
        }
    }

    public void GoToPlayer(){
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < pursueDistance){
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("isAware", true);
        }
        else{
            navMeshAgent.isStopped = true;
            animator.SetBool("isAware", false);
        }
    }

    public void Damage(){
        player.GetComponent<Character>().GetDamage(attackDamage);
    }

    public void CloseToPlayer(){
        
        
    }
}
