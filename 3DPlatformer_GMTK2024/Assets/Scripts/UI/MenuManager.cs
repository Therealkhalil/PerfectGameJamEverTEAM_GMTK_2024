using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Setting")]
    private const float MAX_GAMETIME = 300.0f;
    [SerializeField] private float gameTime = MAX_GAMETIME;

    private void Start()
    {
        StartCoroutine(UpdateTimerText());
    }
    
    private void Update()
    {
        // TODO : Show setting popup
        if (_input.escAction)
        {
            Debug.Log("ESC Pressed");
            _input.ChangeCursorInput(false);
            _playerInput.enabled = false;
            
            PopupObj.SetActive(true);
            _input.escAction = false;
            
            GameManager.instance.StopTime();
        }
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
        _input.ChangeCursorInput(true);
        _playerInput.enabled = true;
        
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
