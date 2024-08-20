using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class GetCoin : MonoBehaviour
{
    int coinCounter = 0;
    int gemCounter = 0;
    // Hookup with UI
    [SerializeField] private MenuManager menuManager;
    
    public delegate void OnCoinCollected();
    public event OnCoinCollected onCoinCollected;
    public FMODUnity.EventReference collectCoinSound;
    public FMODUnity.EventReference collectGemSound;


    private void Start()
    {
        menuManager.SetCoinText(coinCounter);
        menuManager.SetGemText(coinCounter);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            coinCounter++;
            menuManager.SetCoinText(coinCounter);

            FMODUnity.RuntimeManager.PlayOneShot(collectCoinSound);

            if (onCoinCollected != null)
            {
                onCoinCollected.Invoke();
            }

            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("Gem"))
        {
            gemCounter++;

            menuManager.SetGemText(gemCounter);

            FMODUnity.RuntimeManager.PlayOneShot(collectGemSound);

            Destroy(collision.gameObject);
        }

    }

}
