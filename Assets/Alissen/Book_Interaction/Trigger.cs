using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
   [SerializeField]
    private Image BOK;
   
   void OnTriggerEnter(Collider other)
   {
    if(other.CompareTag("Player"))
    {
        BOK.enabled=true;
    }
   }
    
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BOK.enabled=false;
        }
    }
}
