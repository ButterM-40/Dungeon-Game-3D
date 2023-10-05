using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Enemy_Controller : MonoBehaviour
{
    public float lookRadius=10f;

    public float attackCooldown = 2.0f;
    private float nextAttackTime = 0.0f;

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
    }
    void OnCollisionEnter(Collision test)
    {
        if(test.gameObject.tag == "Player")
        {
            Debug.Log("ASMDASD");
        }
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
            if (Time.time >= nextAttackTime)
            {
            m_Animator.SetBool("Attacking",true);
            target.GetComponent<Player>().TakeDamage(10);
            nextAttackTime = Time.time+attackCooldown;
            }
            else
            {
             m_Animator.SetBool("Attacking",false);
            }
           }
        }
    }
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
