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
            GameManager.instance.CanChangeScene();
            GameManager.instance.renderLoadScreen = true;
        }
    }

}
