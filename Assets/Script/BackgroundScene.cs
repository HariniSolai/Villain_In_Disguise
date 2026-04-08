using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BackgroundScreen : MonoBehaviour
{
    public void NextBtn()
    {
        SceneManager.LoadScene(2);
    }
}
