using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField]
    private GameObject player;
    private PlayerController _playerInput;
    private StarterAssetsInputs _input;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject pauseObj;
    [SerializeField] private GameObject optionObj;

    [Header("Setting")]
    private const float MAX_GAMETIME = 300.0f;
    [SerializeField] private float gameTime = MAX_GAMETIME;

    private void Awake()
    {
        _playerInput = player.GetComponent<PlayerController>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        StartCoroutine(UpdateTimerText());
    }
    
    private void Update()
    {
        if (_input.escAction)
        {
            _input.escAction = false;
            Debug.Log("ESC Pressed");

            if (!pauseObj.activeSelf)
            {
                pauseObj.SetActive(true);
                
            
                ChangePlayerInputSetting(false);
            
                GameManager.instance.StopTime();
            }
            else
            {
                Event_Resume();
            }
            
        }

        if (_input.tabAction)
        {
            Debug.Log("TAB Pressed");
            _input.tabAction = false;

            if (!optionObj.activeSelf)
            {
                optionObj.SetActive(true);

                ChangePlayerInputSetting(false);
            
                GameManager.instance.StopTime();
            }
            else
            {
                Event_CloseOption();
            }
            
        }
    }

    public void ChangePlayerInputSetting(bool value)
    {
        _input.ChangeCursorInput(value);
        _playerInput.enabled = value; 
    }
    
    public void Event_CloseOption()
    {
        Debug.Log("Close Option");
        optionObj.SetActive(false);
        GameManager.instance.ResumeTime();
        ChangePlayerInputSetting(true);
    }

    /// <summary>
    /// Functions for pause 
    /// </summary>
    public void Event_Quit()
    {
        // TODO : Scene change to title
        Debug.Log("Quit");
        // GameManager.instance.SceneChange(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Event_Resume()
    {
        GameManager.instance.ResumeTime();
        ChangePlayerInputSetting(true);
        
        pauseObj.SetActive(false);
        
    }
    
    /// <summary>
    /// Timer function
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateTimerText()
    {
        while (gameTime > 0)
        {
            gameTime--;
            if (gameTime < 0)
            {
                // TODO : Game end
                gameTime = 0;
                yield break;
            }

            timeText.text = gameTime.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    public void SetCoinText(int count)
    {
        coinText.text = count.ToString();
    }
    
}
