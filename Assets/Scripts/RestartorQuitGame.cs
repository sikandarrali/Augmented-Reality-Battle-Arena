using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartorQuitGame : MonoBehaviour
{

    public GameObject panelGameOver, inGameUI;

    public void OnClick_BackToHome()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }

}