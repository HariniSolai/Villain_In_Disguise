using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OnQuitBtn()
    {
        Application.Quit();
    } 
}
