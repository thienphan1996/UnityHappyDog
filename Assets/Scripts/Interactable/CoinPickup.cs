using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int scoreValue = 1;
    [SerializeField] AudioClip coinPickupSfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other is CapsuleCollider2D)
        {
            FindObjectOfType<PlayerSession>().IncreaseScore(scoreValue);
            AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
