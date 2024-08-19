using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; 

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
            _input.escAction = false;
        }
    }

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
