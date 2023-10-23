using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Resume()
    {
        GetComponent<AudioSource>().Play();

        var playerSession = FindObjectOfType<PlayerSession>();

        if (playerSession != null)
        {
            playerSession.GameOverResume();
        }
    }

    public void Next()
    {
        GetComponent<AudioSource>().Play();

        var playerSession = FindObjectOfType<PlayerSession>();

        if (playerSession != null)
        {
            playerSession.GameOverNext();
        }
    }

    public void GoHome()
    {
        GetComponent<AudioSource>().Play();

        var playerSession = FindObjectOfType<PlayerSession>();

        if (playerSession != null)
        {
            Destroy(playerSession.gameObject);
        }

        SceneManager.LoadScene(0);
    }
}
