using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.CanChangeScene();
    }

    private void OptionsMenu()
    {
        throw new NotImplementedException();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
