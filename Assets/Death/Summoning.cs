using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boss;
    public GameObject leftDoor;
    public GameObject rightDoor;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the trigger event is with the player
        {
            Boss.SetActive(true);
        }
        
        GameObject left = Instantiate(leftDoor, new Vector3(-0.836212218f,0.214867115f,-6.87507057f), Quaternion.Euler(0, 90, 0));
        GameObject right = Instantiate(rightDoor, new Vector3(-0.836212158f,0.214866638f,-3.6751442f), Quaternion.Euler(0, 90, 0));
        Destroy(gameObject);
    }
    void Start(){
        Boss.SetActive(false);
    }
}
