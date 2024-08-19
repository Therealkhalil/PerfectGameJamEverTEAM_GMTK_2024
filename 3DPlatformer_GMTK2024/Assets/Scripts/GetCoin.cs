using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GetCoin : MonoBehaviour
{
    int coinCounter = 0;

    // Hookup with UI
    [SerializeField] private MenuManager menuManager;
    
    public delegate void OnCoinCollected();
    public event OnCoinCollected onCoinCollected;
    public AudioClip collectCoinSound;

    private void Start()
    {
        menuManager.SetCoinText(coinCounter);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            coinCounter++;
            // Debug.Log("Coins: " + coinCounter);
            menuManager.SetCoinText(coinCounter);

            AudioSource.PlayClipAtPoint(collectCoinSound, collision.transform.position, 1);

            if (onCoinCollected != null)
            {
                onCoinCollected.Invoke();
            }

            Destroy(collision.gameObject);

        }

    }

}
