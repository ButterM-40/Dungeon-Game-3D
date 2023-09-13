using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    NavMeshAgent agent;
    // MoveTo.cs
    public Collider StartAttackZone;
    private bool isTriggered = false;
    public Transform goal;
    private int health;
    private Animator animator;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    bool alreadyAttacked = false;
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    private float nextAttackTime;

    public Transform attackPoint;
    public LayerMask playerLayer;
    //Attacking
    public float timeBetweenAttacks;
    bool isAttacking;

    //States
    public float sightRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake(){
        agent = GetComponent<NavMeshAgent>();
        Transform childTransform = transform.Find("Boss");
        if(childTransform != null){
            animator = childTransform.Find("NPC_2017").GetComponent<Animator>();
            if(animator != null){
                Debug.Log("Works Animation");
            }else{
                Debug.LogError("Child Animator not found");
            }
        }else{
            Debug.LogError("Child does not have an Animator Component.");
        }
    }

    private void Update(){
        
        if(isTriggered){
            StartCoroutine(AttackPlayerSkill3());
        }else{
            ChasePlayer();
            
        }
        
    }

    private void Patroling(){

    }
    private void ChasePlayer(){
        agent.SetDestination(player.position);
        animator.SetBool("isActive", false);
        animator.SetInteger("isSkill", 0);
    }
    IEnumerator AttackPlayerSkill3(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            animator.SetBool("isActive", true);
            animator.SetInteger("isSkill", 3);
            //Attack Code Here
            //Not Implemented Yet
            //Needs Player Healths and My Health
            SphereCollider[] colliders = transform.Find("AttackZone3").GetComponentsInChildren<SphereCollider>();
            foreach (SphereCollider collider in colliders)
            {
                collider.enabled = true;
                StartCoroutine(Skill3(collider));
            }
            yield return new WaitForSeconds(5.0f);
            //animator.SetBool("isActive", true);
            //animator.SetInteger("isSkill", 3);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    IEnumerator Skill3(SphereCollider collider){
        yield return new WaitForSeconds(1.0f);
        collider.enabled = false;
    }
    private void ResetAttack(){
        alreadyAttacked = false;
        isTriggered = false;
        //animator.SetBool("isActive", false);
        //animator.SetInteger("isSkill", 0);
    }
    //Attack Triggers
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            isTriggered = true;
            Debug.Log("Detected Player");
            other.gameObject.GetComponent<Player>().TakeDamage(20);

        }
    }
    void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "Player"){
            isTriggered = true;
            Debug.Log("Detected Player");
        }
    }
}
