using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
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
    Vector3 moveDir;
    void Start(){

        animator = GetComponent<Animator>();
        animator.SetBool("IsAttacking",false);
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
            
            //Moving Animations
            if(!Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButtonDown(0)){
                speed = 6.0f;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsRunning", false);
                
            }else{

                speed = 10f;
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsMoving", false);
            }

            if(Input.GetMouseButtonDown(0))
            {
                animator.SetBool("IsAttacking", true);
            }

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else{
            //Animation for the idle
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
            if (Input.GetMouseButtonDown(0))
                animator.SetBool("IsAttacking", true);
        }

        if(Input.GetMouseButtonUp(0)){
                animator.SetBool("IsAttacking", false);
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
    IEnumerator isRollingCoroutine(){
    // Play rolling animation
    animator.SetBool("IsRolling", true);
    animator.SetBool("IsRunning", false);
    animator.SetBool("IsMoving", false);
    Debug.Log("Running Test");
    yield return new WaitForSeconds(0.2f);
    // Get the character's current forward direction (assuming it's facing forward)
    Vector3 rollDirection = transform.forward; // Adjust this as needed

    // Apply a force to move the character in the rollDirection
    float rollSpeed = 5.0f; // Adjust this as needed
    float rollingDuration = 2.0f; // Adjust this as needed

    // Calculate the distance to move during the roll based on the rollSpeed and duration
    float rollDistance = rollSpeed * rollingDuration;

    // Get the CharacterController component
    CharacterController characterController = GetComponent<CharacterController>();

    // Move the character in the rollDirection for the specified distance
    while (rollDistance > 0)
    {
        float moveDistance = rollSpeed * Time.deltaTime;
        characterController.Move(rollDirection * moveDistance);
        rollDistance -= moveDistance;

        yield return null;
    }
    animator.SetBool("IsRolling", false);

    isRolling = false;
    }
    // public void updateTransform(Transform newPosition){
    //     transform = newPosition;
    // }
}


