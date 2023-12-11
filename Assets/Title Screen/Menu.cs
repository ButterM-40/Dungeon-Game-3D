using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string newGameScene;
    // Start is called before the first frame update
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void OnQuitButton()
    {
        Application.Quit();
    }
}