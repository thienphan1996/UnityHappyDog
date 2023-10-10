using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float delayLoadScene = 1f;
    [SerializeField] AudioClip finishSfx;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        AudioSource.PlayClipAtPoint(finishSfx, Camera.main.transform.position);

        yield return new WaitForSecondsRealtime(delayLoadScene);
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

        FindObjectOfType<PlayerSession>().UpdateLevel(nextSceneIndex);
    }
}
