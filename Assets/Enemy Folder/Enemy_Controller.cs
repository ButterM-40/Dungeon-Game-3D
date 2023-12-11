using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Enemy_Controller : MonoBehaviour
{
    public float lookRadius=10f;

    public float attackCooldown = 2.0f;
    private float nextAttackTime = 0.0f;
    public float delayAttack = .2f;

    private float attack;

    // ***************
    public float attackDelay = 2f; // Time delay before the enemy inflicts damage
    private float attackTimer = 0f;
    // ***************




    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator m_Animator;
    void Start()
    {
        target = Player_Manager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        m_Animator=GetComponent<Animator>();
        attack = delayAttack;
    }
    void Update()
    {
        float distance = Vector3.Distance(target.position,transform.position);
        if (distance <= lookRadius)
        {
           agent.SetDestination(target.position);
           if (distance <= agent.stoppingDistance)
           {
            FaceTarget();
            m_Animator.SetBool("Attacking",true);
            if (m_Animator.GetBool("Attacking") == true)
            {
                attackTimer += Time.deltaTime;
            }
            if (attackTimer >= attackDelay)
            {
            target.GetComponent<Player>().TakeDamage(10);
            attackTimer=0f;
             m_Animator.SetBool("Attacking",false);
            }
           }
           else
           {
              m_Animator.SetBool("Attacking",false);
              attackTimer=0f;
           } 
        }
    }
    // void Update()
    // {
    //     float distance = Vector3.Distance(target.position,transform.position);
    //     if (distance <= lookRadius)
    //     {
    //        agent.SetDestination(target.position);
    //        if (distance <= agent.stoppingDistance)
    //        {
    //         attack -= Time.deltaTime;
    //         FaceTarget();
    //         if (Time.time >= nextAttackTime)
    //         {
    //         m_Animator.SetBool("Attacking",true);
    //         if (attack <= 0f)
    //         {
    //         target.GetComponent<Player>().TakeDamage(10);
    //          attack=delayAttack;
    //         }
    //         nextAttackTime = Time.time+attackCooldown;
    //         }
    //         else
    //         {
    //          m_Animator.SetBool("Attacking",false);
    //         }
    //        }
    //      else
    //         {
    //             attack=delayAttack;
    //         }
    //     }
    // }
    void FaceTarget()
    {
        Vector3 direction = (target.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation=Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
