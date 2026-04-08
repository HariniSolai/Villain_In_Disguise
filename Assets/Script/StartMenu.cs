using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnPlayBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    } 
}
