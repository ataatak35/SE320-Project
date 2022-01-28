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
    public int deadCount;
    public bool isAttacking;
    
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
            Invoke("DecreaseZombieAmount", 1);
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

    public void DecreaseZombieAmount(){
        GameManager.instance.zombieAmount--;
    }

    public void GoToPlayer(){
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (GameManager.instance.isCreating || distanceToPlayer < pursueDistance){
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = 5f;
            navMeshAgent.SetDestination(player.transform.position);
            animator.SetBool("isAware", true);
        }
    }
    
    public void Damage(){
        player.GetComponent<Character>().GetDamage(attackDamage);
    }

    public IEnumerator wait(){
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(5f);
        navMeshAgent.isStopped = false;
    }
}
