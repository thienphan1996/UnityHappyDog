using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] List<Image> liveImages;
    [SerializeField] Sprite aliveSprite;
    [SerializeField] Sprite deadSprite;

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

        var scenPersist = FindObjectOfType<ScenePersist>();

        if (scenPersist != null)
        {
            scenPersist.ResetScene();
        }
    }

    private void Start()
    {
        SetLiveImages();

        scoreText.text = score.ToString();
        levelText.text = "Level " + playerLevel.ToString();
    }

    private void SetLiveImages()
    {
        for (var i = 0; i < liveImages.Count; i++)
        {
            if (i < playerLives)
            {
                liveImages[i].sprite = aliveSprite;
            }
            else
            {
                liveImages[i].sprite = deadSprite;
            }
        }
    }

    public void GameOverResume()
    {
        if (playerLives == 0)
        {
            playerLives = 1;

            SetLiveImages();
        }

        SceneManager.LoadScene(playerLevel);
    }

    public void GameOverNext()
    {
        playerLives = 3;
        playerLevel++;

        SetLiveImages();
        UpdateLevel(playerLevel);

        SceneManager.LoadScene(playerLevel);
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

        SetLiveImages();

        return playerLives;
    }

    public void RestartGame()
    {
        var scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScene();
        }
    }
}
