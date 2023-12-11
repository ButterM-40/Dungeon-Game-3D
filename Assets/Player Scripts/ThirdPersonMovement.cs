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
    // I added a prefab reference thing for the Animator component (I can attach the slash here and call it later in the code )
    public GameObject prefab;
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
                /// I edited this portion of the code so I can have the slash effect be done at the same time when we attack 
                 animator.SetBool("IsAttacking", true);
                 GameObject instance = Instantiate(prefab, transform.position, transform.rotation);
                 Animator prefabAnimator = instance.GetComponent<Animator>();
                 prefabAnimator.SetBool("IsAttacking", true);

               
            }

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else{
            //Animation for the idle
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
            if (Input.GetMouseButtonDown(0))
            /// I edited this portion of the code so I can have the slash effect be done at the same time when we attack 
            {
            animator.SetBool("IsAttacking", true);
            GameObject instance = Instantiate(prefab, transform.position, transform.rotation);
            Animator prefabAnimator = instance.GetComponent<Animator>();
            prefabAnimator.SetBool("IsAttacking", true);
            }
                
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

    // Get the character's current forward direction (assuming it's facing forward)
    Vector3 rollDirection = transform.forward; // Adjust this as needed

    // Apply a force to move the character in the rollDirection
    float rollSpeed = 5.0f; // Adjust this as needed
    float rollingDuration = 1.4f; // Adjust this as needed

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
 public void Die()
  {
    //Should be implemented in next sprint
   // enemy_controller.enabled=false;
    Debug.Log("WORKED");
    animator.SetTrigger("Dead");
    StartCoroutine(DestroyAfterDelay(3f));
  }
  IEnumerator DestroyAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    Destroy(gameObject);
  }
  public void TakeDamage()
  {
    animator.SetTrigger("Hit");
  }

}



