using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameSplashScreen : MonoBehaviour
{
    public void OnClick_StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
