using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Damage : MonoBehaviour
{
        Transform target;
        Animator m_Animator;
         private float DespawnDelay = 3f;
         public int  Damage=10;

    void Start()
    {
        target = Player_Manager.instance.player.transform;
        StartCoroutine(DespawnBullet());
        //m_Animator=GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
           // print("HIT");
           target.GetComponent<Player>().TakeDamage(Damage);
           Destroy(gameObject);
        }
    }
    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(DespawnDelay);
        Destroy(gameObject);
    }
    
}
