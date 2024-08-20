using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class OptionManager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("index - 0: on, 1: off")]
    [SerializeField] private Image[] screenButtons;
    [SerializeField] private Sprite[] buttonSprites;

    public FMOD.Studio.VCA Music;
    public FMOD.Studio.VCA Sound;

    private void Start()
    {
        Invoke("SoundDelay", 1);

        UpdateButtonSprites();

    }
    private void SoundDelay()
    {
        Music = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        Sound = FMODUnity.RuntimeManager.GetVCA("vca:/Sound");
    }
    public void Event_OnSetVolumeMusic(float newVolume)
    {
        Music.setVolume(newVolume);
    }
    public void Event_OnSetVolumeSFX(float newVolume)
    {
        Sound.setVolume(newVolume);
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
