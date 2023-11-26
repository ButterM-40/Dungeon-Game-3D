
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHoming : MonoBehaviour
{
    private Transform target; // Reference to the target position.
    public float moveSpeed = 5.0f;

    private bool isMoving = true;
    private Transform position1;
    private void Update()
    {
        if (isMoving && target != null)
        {
            //Debug.Log("Run");
            // Calculate the direction to move towards the target.
            Vector3 moveDirection = (target.position - transform.position).normalized;

            // Move the object towards the target position.
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Check if we're close enough to stop following.
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < 0.1f)
            {
                isMoving = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with a trigger collider.
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(20);
            //Debug.Log("Meow");
            isMoving = false; // Stop moving when a trigger is hit.
            Destroy(gameObject);
        }
    }
    public void SetParameters(Transform target1)
    {
        target = target1;
        position1 = target;

    }
}
