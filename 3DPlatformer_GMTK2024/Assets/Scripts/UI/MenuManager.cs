using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Input System")]
#if ENABLE_INPUT_SYSTEM 
    [SerializeField] private PlayerInput _playerInput;
#endif
    [SerializeField] private StarterAssetsInputs _input;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject PopupObj;
    [SerializeField] private DOTweenAnimation _animation;

    [Header("Setting")]
    private const float MAX_GAMETIME = 300.0f;
    [SerializeField] private float gameTime = MAX_GAMETIME;

    private void Start()
    {
        StartCoroutine(UpdateTimerText());
    }
    
    private void Update()
    {
        if (_input.escAction)
        {
            Debug.Log("ESC Pressed");
            PopupObj.SetActive(true);
            _input.escAction = false;
            
            ChangePlayerInputSetting(false);
            
            GameManager.instance.StopTime();
        }

        if (_input.tabAction)
        {
            Debug.Log("TAB Pressed");
            _input.tabAction = false;
            _animation.gameObject.SetActive(true);

            ChangePlayerInputSetting(false);
            
            GameManager.instance.StopTime();
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
        _animation.gameObject.SetActive(false);
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
        
        PopupObj.SetActive(false);
        
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
