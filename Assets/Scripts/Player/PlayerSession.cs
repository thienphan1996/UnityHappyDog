using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelText;

    private int score = 0;
    private int playerLives = 3;
    private int playerLevel = 1;
    public int PlayerLevel => playerLevel;

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

    public void UpdateLevel(int level)
    {
        playerLevel = level;

        if (level == 0)
        {
            playerLevel = 1;
        }

        levelText.text = "Level " + playerLevel.ToString();

        FindObjectOfType<ScenePersist>().ResetScene();
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        levelText.text = "Level " + playerLevel.ToString();
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public int PlayerDeath()
    {
        if (playerLives > 0)
        {
            playerLives--;
        }
        else
        {
            playerLives = 0;
        }

        livesText.text = playerLives.ToString();

        return playerLives;
    }

    public void RestartGame()
    {
        Destroy(gameObject);

        FindObjectOfType<ScenePersist>().ResetScene();
    }
}
