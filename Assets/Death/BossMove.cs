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
    public float _gravityACD = 10.0f;
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
        animator = transform.GetComponent<Animator>();
            if(animator != null){
                Debug.Log("Works Animation");
            }else{
                Debug.LogError("Child Animator not found");
            }
    }

    private void Update(){
        float distance = Vector3.Distance(player.position,transform.position);

        if(distance <= 2f && !alreadyAttacked){
            StartCoroutine(AttackPlayerSkill3());
        }else if(distance <= 20f && _gravityACD <= 0f && !alreadyAttacked){
            StartCoroutine(GravitySkill4());
        }
        else{
            ChasePlayer();
        }
        Cooldowns();
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
        animator.SetBool("isActive", false);
        animator.SetInteger("isSkill", 0);
    }
    //Basic Scyther Attack
    IEnumerator AttackPlayerSkill3(){
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        
        Vector3 direction = (player.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation=Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime * 5f);

        if(!alreadyAttacked){
            animator.SetBool("isActive", true);
            animator.SetInteger("isSkill", 3);
        
            yield return new WaitForSeconds(5.0f);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    IEnumerator GravitySkill4(){
        agent.SetDestination(transform.position);
        
        Vector3 direction = (player.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation=Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime * 5f);

        if(!alreadyAttacked){
            animator.SetBool("isActive", true);
            animator.SetInteger("isSkill", 4);
            yield return new WaitForSeconds(.5f);
            while(true){
                //  Calculate the pull direction vector
                Vector3 pullDirection = (transform.position-player.position);
                //  Normalize it and multiply by the force modifier
                Vector3 pullForce = pullDirection.normalized * 1.0f;
                float distanceToHand = Vector3.Distance (transform.position, player.position);
                if (player.GetComponent<Rigidbody>().velocity.magnitude < 5f) {
                    //  Add force that takes the object's mass into account
                    player.GetComponent<Rigidbody>().AddForce (pullForce, ForceMode.Force);
                } else {
                    // Set a constant velocity to the object
                    player.GetComponent<Rigidbody>().velocity = pullDirection.normalized * 5f;
                }
                Debug.Log(distanceToHand);
                if(distanceToHand <= 2f){
                    Debug.Log("Broke Out");
                    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    break;
                }
    
                yield return null;
            }
            _gravityACD = 10f;
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }
    //Attack Triggers
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            isTriggered = true;
            Debug.Log("Detected Player");
            other.gameObject.GetComponent<Player>().TakeDamage(20);
        }
    }
    public void Cooldowns(){
        _gravityACD -= Time.deltaTime;
    }
}
