using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float speed = 6.0f;

    private Animator animator;
    private bool isRolling = false;
    private float turnSmoothVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f && !isRolling)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Apply gravity only when not rolling
            if (!isRolling)
            {
                moveDir.y = -100f * Time.deltaTime;
            }
            else
            {
                moveDir.y = 0f; // No gravity while rolling
            }

            // Set speed based on input
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6.0f;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsRunning", false);
            }
            else
            {
                speed = 10f;
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsMoving", false);
            }

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            // Animation for idle
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isRolling)
            {
                isRolling = true;
                StartCoroutine(isRollingCoroutine());
            }
        }
    }

    IEnumerator isRollingCoroutine()
    {
        // Play rolling animation
        animator.SetBool("IsRolling", true);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsMoving", false);
        Debug.Log("Running Test");

        // Wait for the rolling animation's duration or use a fixed time
        float rollingDuration = 1.0f; // Adjust this as needed
        yield return new WaitForSeconds(rollingDuration);

        // Stop rolling and return to normal movement
        animator.SetBool("IsRolling", false);
        
        isRolling = false;
    }
}
