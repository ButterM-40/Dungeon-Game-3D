using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawner_Range : MonoBehaviour
{

    public float lookRadius=10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
