using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Detect_Script : MonoBehaviour
{
    bool detected = false;
    GameObject target;
    public Transform enemy;
    public GameObject bullet;
    public Transform shootPoint;
    public float shootSpeed = 10f;
    public float timetoShoot=1.3f;
    public float Stay_Still=1.0f;
    float originalTime;

    float StillTime;

    //DETECTION
    public float lookRadius=10f;

    public GameObject EnemyNav;
    private NavMeshAgent agent;
    Transform target1;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        target1 = Player_Manager.instance.player.transform;
        agent = EnemyNav.GetComponent<NavMeshAgent>();
        originalTime=timetoShoot;
        StillTime = Stay_Still;
        m_Animator = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     float distance = Vector3.Distance(target1.position, enemy.transform.position);
        if (distance <= lookRadius)
        {
            //agent.SetDestination(target1.position);
            enemy.LookAt(target1.transform);
            detected = true;
        }
        else
        {
            detected=false;
        }
        if (distance <= lookRadius/2)
        {
        enemy.Translate(Vector3.forward * -agent.speed * Time.deltaTime);
        }

    }
    private void FixedUpdate()
    {
        if (detected)
        {
            timetoShoot -= Time.deltaTime;
            
            if (timetoShoot < 0 )
            {
               m_Animator.SetBool("Attacking",true);
                ShootPlayer();
                timetoShoot=originalTime;
            }
            else
            {   
              m_Animator.SetBool("Attacking",false);
            }
             if (agent.speed == 0)
             {
                 Stay_Still -= Time.deltaTime;
                 if (Stay_Still < 0)
                 {
                agent.speed = 1f;
                Stay_Still = StillTime;
                 }
            }
        }
    }
    private void ShootPlayer()
    {
         agent.speed=0f;
        GameObject currentBullet = Instantiate(bullet,shootPoint.position, shootPoint.rotation);
        Rigidbody rig = currentBullet.GetComponent<Rigidbody>();
        rig.AddForce(transform.forward*shootSpeed,ForceMode.VelocityChange);

    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
