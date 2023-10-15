using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest_PopUp : MonoBehaviour
{
    [SerializeField]
    private Image Scroll;
   private Animator animator;
   
   private void Start()
   {
    animator=GetComponent<Animator>();
   }
  
   void OnTriggerEnter(Collider other)
   {
    if(other.CompareTag("Player"))
    {
       
        animator.SetBool("isOpen", true);
        Scroll.enabled=true;
       
    }
   }
    
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Scroll.enabled=false;
            
        }
    }
    }

