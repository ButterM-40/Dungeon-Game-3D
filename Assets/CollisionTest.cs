using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    float timePassed = Time.deltaTime;
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
        if (other.gameObject.tag == "Player")
        {
            if (timePassed >= AttackCD)
            {
            other.GetComponent<Player>().TakeDamage(10);
            print("Attack");
            print(Time.deltaTime);
            timePassed=0;
            }
        }
    }
}
