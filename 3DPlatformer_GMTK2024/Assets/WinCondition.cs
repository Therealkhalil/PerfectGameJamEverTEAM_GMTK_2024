using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class WinCondition : MonoBehaviour
{
    public Transform player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.SceneChangeAsync("Cutscene End");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.Distance(transform.position, player.position);

    }
}
