using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI textScore, textLives, msgGameOver;

    public GameObject panelGameOver, inGameUI;

    public GameObject characater, enemy;

    public static int score = 0;
    public static int playerLives = 3;
    public static bool gameOver = false;

    void Start()
    {
        panelGameOver.SetActive(false);
    }

    void Update()
    {
        PopulateScoring();

        if(gameOver == true)
        {
            inGameUI.SetActive(false);
            panelGameOver.SetActive(true);

            // Set Static Vatiables after delay
            StartCoroutine(SetGameVariablesAfterDelay());
            StartCoroutine(StopGame());
        }

    }

    private void PopulateScoring()
    {
        if(gameOver == false)
        {
            textScore.text = "Enemies: " + score + " / 10";
            textLives.text = "Lives: " + playerLives;
            GameOverCheck();
        }
    }

    public void GameOverCheck()
    {
        if (score == 10)
        {
            msgGameOver.text = "YOU WIN!!!";
            gameOver = true;
        }
        if(playerLives == 0)
        {
            msgGameOver.text = "YOU LOSE :(";
            gameOver = true;
        }
    }

    IEnumerator SetGameVariablesAfterDelay()
    {
        yield return new WaitForSeconds(2);
        score = 0;
        playerLives = 3;
        gameOver = false;

    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
    }

}
