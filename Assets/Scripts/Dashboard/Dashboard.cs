using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{
    [SerializeField] AudioClip buttonSfx;
    public void Play()
    {
        AudioSource.PlayClipAtPoint(buttonSfx, Camera.main.transform.position);

        var playerSession = FindObjectOfType<PlayerSession>();

        if (playerSession != null)
        {
            playerSession.RestartGame();
        }

        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        AudioSource.PlayClipAtPoint(buttonSfx, Camera.main.transform.position);

        var playerSession = FindObjectOfType<PlayerSession>();

        if (playerSession == null)
        {
            SceneManager.LoadScene(1);
            return;
        }

        var playerLevel = playerSession.PlayerLevel;

        if (playerLevel == 0)
        {
            playerLevel = 1;
        }

        SceneManager.LoadScene(playerLevel);
    }
}
