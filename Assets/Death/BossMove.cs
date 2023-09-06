using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    // MoveTo.cs
      
    public Transform goal;
    private int health;
    
    //Boss Movements Implementations
    // public Transform _attack;
    // private IAttack _attackComponent;
    // private Patrol _patrol;
    // private Awareness _aware;
    // private Chase _chase;


    // void Start () {
        
    //     agent.destination = goal.position; 
    // }

    // void Update(Vector2 end){
    //     agent.destination = goal.position; 
    // }
    
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    bool alreadyAttacked = false;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool isAttacking;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake(){
        //player = GameObject.Find("Third Person Player").Transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        ChasePlayer();
    }

    private void Patroling(){

    }
    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }
    private void AttackPlayerSkill1(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){

            //Attack Code Here
            //Not Implemented Yet
            //Needs Player Healths and My Health
            
            
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }
    // public void TakeDamage(int damage){
    //     health -= damage;
    //     if(health <= 0) {
    //         Invoke(nameof(DestroyBoss), .5f);
    //     }
    // }
    // private void DestoryBoss(){
    //     Destory(gameObject);
    // }
}
