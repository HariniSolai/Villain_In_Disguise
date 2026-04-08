using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InsructionScreen : MonoBehaviour
{
    public void OnGoToGameBtn()
    {
        SceneManager.LoadScene(1);
    }
}
