<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boss;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the trigger event is with the player
        {
            Boss.SetActive(true);
        }
    }
    void Start(){
        Boss.SetActive(false);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boss;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the trigger event is with the player
        {
            Boss.SetActive(true);
        }
    }
    void Start(){
        Boss.SetActive(false);
    }
}
>>>>>>> 63f50c2d7a97a4281c8cff50ce74765b827a7a91
