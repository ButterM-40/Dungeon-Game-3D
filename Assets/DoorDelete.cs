using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDelete : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        Destroy(gameObject);
    }
}
