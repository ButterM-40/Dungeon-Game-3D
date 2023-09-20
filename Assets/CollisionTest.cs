using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
  /*  
    float AttackCD = 100000.0f;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            print("Enter");
        }
    }
    void OnTriggerStay(Collider other)
    {
        float timePassed = Time.deltaTime;
        if (other.gameObject.tag == "Player")
        {
            //if (timePassed >= AttackCD)
            //{
            other.GetComponent<Player>().TakeDamage(10);
            print("Attack");
            print(Time.deltaTime);
            //timePassed=0;
            //}
        }
    }
    */

    float AttackCD = 1.0f; // Adjust this value to set the cooldown duration
    float timePassed = 0.0f;
    bool canAttack = true;

    void Update()
    {
        // Update the timePassed continuously
        timePassed += Time.deltaTime;

        // Check if the cooldown has ended
        if (timePassed >= AttackCD)
        {
            canAttack = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Enter");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && canAttack)
        {
            // Damage the player
            other.GetComponent<Player>().TakeDamage(10);
            print("Attack");

            // Reset the cooldown timer
            timePassed = 0.0f;
            canAttack = false;
        }
    }
}
