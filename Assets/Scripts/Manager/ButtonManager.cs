using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("StartScene");
    }
}
