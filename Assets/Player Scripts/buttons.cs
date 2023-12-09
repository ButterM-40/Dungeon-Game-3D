using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class buttons : MonoBehaviour
{
    public void ReturnToGame(){
        SceneManager.LoadScene(1);
   }
   public void HomenGame(){
        SceneManager.LoadScene(0);
   }
   public void BossGame(){
        SceneManager.LoadScene(2);
   }
}