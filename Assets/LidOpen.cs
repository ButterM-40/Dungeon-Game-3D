using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidOpen : MonoBehaviour
{
    // Start is called before the first frame update
 private Animator animator;

    private void Start()
    {
        // Get the Animator component from the object with the animation.
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player.
        if (other.CompareTag("Player"))
        {
            // Trigger the animation.
            animator.SetBool("isOpen", true);
        }
    }
}
