using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_Player : MonoBehaviour
{
    private Spawn_Enemy spawn_enemy;
     Transform target1;
     private Player_Spawner_Range player_spawner_range;
     float templook=50f;
     private float test;
    
    void Start()
    {
    target1 = Player_Manager.instance.player.transform;
    spawn_enemy = GetComponent<Spawn_Enemy>();
    //player_spawner_range = GetComponent<Player_Spawner_Range>();
//    test = player_spawner_range.lookRadius;
    }

    // Update is called once per frame
    void Update()
    {
    float distance = Vector3.Distance(target1.position, transform.position); 
   // Debug.Log(player_spawner_range.lookRadius);
        if (distance <= templook)
        {
            //agent.SetDestination(target1.position);
            spawn_enemy.enabled = true;
        }
        else
        {
            spawn_enemy.enabled = false; 
        }
    }

    // void OnDrawGizmosSelected ()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position,templook);
    // }
}
