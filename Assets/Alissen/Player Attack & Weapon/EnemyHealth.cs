using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  Animator animator;
  public int startingHealth=100;
  public int currentHealth;
  public int damagerPerSwing=10;
  public GameObject Drop;

  void Awake()
  {
    animator=GetComponent<Animator>();
    currentHealth=startingHealth;

  }

  void OnTriggerEnter(Collider other)
  {
    if(other.CompareTag("Weapon"))
    {
        currentHealth-=damagerPerSwing;
        if(currentHealth<=0)
            Die();
    }
  }

  void Die()
  {
    //Should be implemented in next sprint
    //animator.SetTrigger("DEATH");
    Debug.Log("Stuff");
    Instantiate(Drop, Vector3.zero, Quaternion.identity, transform);
    Destroy(gameObject, 2f);

  }

}
