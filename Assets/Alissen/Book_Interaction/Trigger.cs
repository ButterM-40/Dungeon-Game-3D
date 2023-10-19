using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    /*
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
    */
    private Canvas canvas;

    private void Start()
    {
        // Find the canvas by its name
        canvas = GameObject.Find("Actual Book").GetComponent<Canvas>();
        
        if (canvas != null)
        {
            // Disable the canvas at the start
            canvas.enabled = false;
        }
        else
        {
            Debug.LogError("Canvas 'Actual Book' not found. Make sure the name is correct.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
            {
                // Enable the canvas when the player enters the trigger zone
                canvas.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
            {
                // Disable the canvas when the player exits the trigger zone
                canvas.enabled = false;
            }
        }
    }
}
