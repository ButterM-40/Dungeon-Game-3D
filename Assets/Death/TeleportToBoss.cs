using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToBoss : MonoBehaviour
{
    void OnTriggerEnter(Collider player){
        if(player.CompareTag("Player")){
            SceneManager.LoadScene(1);
        }
    }
}
