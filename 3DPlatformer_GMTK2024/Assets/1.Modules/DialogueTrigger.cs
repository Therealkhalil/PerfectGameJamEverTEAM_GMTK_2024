using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Localization;
// using UnityEngine.Localization.Settings;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    private static DialogueTrigger instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Trigger in the scene");
        }

        instance = this;
    }
    public static DialogueTrigger GetInstance()
    {
        return instance;
    }

    // Dialogue Triggers (JSON)
    public void TriggerDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            TriggerDialogue();
        }
    }
}
