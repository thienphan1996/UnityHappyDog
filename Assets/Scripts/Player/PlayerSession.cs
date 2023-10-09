using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;
    private int playerLives = 3;

    private void Awake()
    {
        var numGameSessions = FindObjectsOfType<PlayerSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public int PlayerDeath()
    {
        playerLives--;
        livesText.text = playerLives.ToString();

        return playerLives;
    }

    public void RestartGame()
    {
        score = 0;
        playerLives = 3;

        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
}
