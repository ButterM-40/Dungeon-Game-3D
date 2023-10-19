using System.Collections;
using System.Collections.Generic;
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
    float originalTime;

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
        m_Animator = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (detected)
        // {
        //     enemy.LookAt(target.transform);
        // }
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
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         detected=true;
    //         target=other.gameObject;
    //     }
    // }
    private void ShootPlayer()
    {
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
