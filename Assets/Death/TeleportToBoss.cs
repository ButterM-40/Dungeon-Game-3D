<<<<<<< HEAD
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
=======
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
>>>>>>> 63f50c2d7a97a4281c8cff50ce74765b827a7a91
