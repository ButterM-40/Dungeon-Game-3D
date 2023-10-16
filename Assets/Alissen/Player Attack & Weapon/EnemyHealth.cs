using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  Animator animator;
  public int startingHealth=100;
  public int currentHealth;
  public int damagerPerSwing=10;

  void Awake()
  {
    animator=GetComponent<Animator>();
    currentHealth=startingHealth;

  }

  void OnTriggerEnter(Collider other)
  {
    if(other.CompareTag("Weapon"))
    {
      Debug.Log("Hit");
        currentHealth-=damagerPerSwing;
        if(currentHealth<=0)
            Die();
    }
  }

  void Die()
  {
    //Should be implemented in next sprint
    //animator.SetTrigger("DEATH");
    Destroy(gameObject, 2f);

  }
}
