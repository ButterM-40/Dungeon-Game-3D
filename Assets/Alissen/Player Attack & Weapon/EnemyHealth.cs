using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  Animator animator;
  private Enemy_Controller enemy_controller;
  public int startingHealth=100;
  public int currentHealth;
  public int damagerPerSwing=10;
  public GameObject Drop;

  void Awake()
  {
    animator=GetComponent<Animator>();
    currentHealth=startingHealth;
   enemy_controller = GetComponent<Enemy_Controller>();
  }
  void OnTriggerEnter(Collider other)
  {
    Debug.Log("Hit");
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
    enemy_controller.enabled=false;
    animator.SetTrigger("Dead");
    Debug.Log("ENEMY DIED");
    StartCoroutine(DestroyAfterDelay(3f));
  }
  IEnumerator DestroyAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    Destroy(gameObject);
    //animator.SetTrigger("DEATH");
    Debug.Log("Stuff");
    Instantiate(Drop, transform.position, Quaternion.identity);
    Drop.transform.SetParent(null);
    Destroy(gameObject, 2f);

  }

}
