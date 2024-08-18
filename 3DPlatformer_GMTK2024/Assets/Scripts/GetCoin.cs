using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetCoin : MonoBehaviour
{
    int coinCounter = 0;

    //TODO: Hookup with UI
    public delegate void OnCoinCollected();
    public event OnCoinCollected onCoinCollected;
    public AudioClip collectCoinSound;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("triggered");
        if (collision.transform.CompareTag("Coin"))
        {
            coinCounter++;
            Debug.Log("Coins: " + coinCounter);

            AudioSource.PlayClipAtPoint(collectCoinSound, collision.transform.position, 1);

            if (onCoinCollected != null)
            {
                onCoinCollected.Invoke();
            }

            Destroy(collision.gameObject);

        }

    }

}
