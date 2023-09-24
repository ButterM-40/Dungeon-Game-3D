using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
  
    
    public CharacterController controller;
    public Transform cam;
    // Update is called once per frame
    public float turnSmoothTime = 0.1f;
    public float speed = 6.0f;
    private Animator animator;
    float turnSmoothVelocity;
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

        if(direction.magnitude >= 0.1f){

            //Calculates the angle at which my character is facing and the camera location. 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            //Adjustment for rotational movement
            moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward ;
            
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
            //Needs to be affected by Gravity Affecting the Y axis
            moveDir.y = -100f * Time.deltaTime;

            //Add a speed boost 
            if(Input.GetMouseButtonDown(0))
            {
                animator.SetBool("IsAttacking", true);
            }
           

            controller.Move(moveDir.normalized * speed * Time.deltaTime);       
            //Debug.Log(moveDir);
            
        }else{
            //Animation for the idle
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
            if (Input.GetMouseButtonDown(0))
                animator.SetBool("IsAttacking", true);
        }
        if(Input.GetMouseButtonUp(0))
            animator.SetBool("IsAttacking", false);
   
    }
    

}


