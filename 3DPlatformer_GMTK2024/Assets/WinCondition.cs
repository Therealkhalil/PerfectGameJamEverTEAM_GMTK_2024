using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class WinCondition : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollisionTriggered");
        if (other.tag == "Player")
        {
            Debug.Log("playerTriggered");
            GameManager.instance.SceneChange("Cutscene End");
            GameManager.instance.CanChangeScene();
            GameManager.instance.renderLoadScreen = true;
        }
    }
}
