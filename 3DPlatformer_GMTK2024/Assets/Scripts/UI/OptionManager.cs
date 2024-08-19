using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("index - 0: on, 1: off")]
    [SerializeField] private Image[] screenButtons;
    [SerializeField] private Sprite[] buttonSprites;

    private void Start()
    {
        UpdateButtonSprites();
    }

    /// <summary>
    /// FullScreen Setting
    /// </summary>
    public void Event_SetFullscreenOn()
    {
        Screen.fullScreen = true;
        UpdateButtonSprites();
    }

    public void Event_SetFullscreenOff()
    {
        Screen.fullScreen = false;
        UpdateButtonSprites();
    }

    private void UpdateButtonSprites()
    {
        if (Screen.fullScreen)
        {
            screenButtons[0].sprite = buttonSprites[0];
            screenButtons[1].sprite = buttonSprites[1];
        }
        else
        {
            screenButtons[0].sprite = buttonSprites[1];
            screenButtons[1].sprite = buttonSprites[0];
        }
    }
    
    
}
