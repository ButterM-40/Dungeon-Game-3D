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
        animator.SetBool("isOpen",false);
    }
   void OnTriggerEnter(Collider other)
   {
    if(other.CompareTag("Player"))
    {
        Scroll.enabled=true;
        animator.SetBool("isOpen", true);
    }
   }
    
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Scroll.enabled=false;
            animator.SetBool("isOpen", false);
        }
    }
}
