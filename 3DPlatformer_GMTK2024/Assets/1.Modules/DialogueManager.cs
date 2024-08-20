using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    // 작을 수록 더 빠른 타이밍 스피드
    [SerializeField] private float typingSpeed = BASIC_TYPE_SPEED;
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject nameFrame;
    [SerializeField] private Animator imageAnimator;
    

    [Header("Game")]
    // [SerializeField] private Button goAreaButton;
    [SerializeField] private Button touchAreaButton;

    [SerializeField] private MenuManager menuManager; 
    private bool isButtonClicked = false;

    private static DialogueManager instance;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;

    // [ const value ]
    private const float BASIC_TYPE_SPEED = 0.04f;
    private const string NAME_TAG = "name";
    private const string IMAGE_TAG = "img"; 

    private void Awake()
    {
        if (instance != null) {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        
        instance = this;

        // dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = true;

    }

    // Put function to touchArea in dialogue
    public void Button_TouchedDialogueArea()
    {
        if (!dialogueIsPlaying) 
        {
            return;
        }

        if (!canContinueToNextLine) {
            isButtonClicked = true;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0) {
            ContinueStory();
        }
       
    }

    public void EnterDialogueMode(TextAsset inkJSON, string knotName=null)
    {
        // ink file load
        currentStory = new Story(inkJSON.text);

        if (knotName != null) {
            currentStory.ChoosePathString(knotName);
        }
        
        menuManager.ChangePlayerInputSetting(false);

        /*
        currentStory.BindExternalFunction ("OnUnlockSlot", () => {
            // @ AdventureManager.GetInstance().DisplayOnUnlockSlot();
        });
        currentStory.BindExternalFunction ("OffUnlockSlot", () => {
            // @ AdventureManager.GetInstance().DisplayOffUnlockSlot();
        });
        currentStory.BindExternalFunction ("OnEventUnlockSlot", (int index) => {
            // @ AdventureManager.GetInstance().DisplayOnEventUnlockSlot(index);
        });
        currentStory.BindExternalFunction ("GetGift", (int index) => {
            // @ AdventureManager.GetInstance().GetGiftItem(index);
        });

        // List<string> progress = AdventureManager.GetInstance().storyProgress;
        // if (progress.Contains("mouse")) {
        //     currentStory.variablesState["is_mouse"] = true;
        //     if (progress.Contains("stranger")) {
        //         currentStory.variablesState["is_stranger"] = true;
        //     }
        // }
        */

        dialogueIsPlaying = true;
        dialogueCanvas.SetActive(true);
        
        // reset
        nameText.text = "";
        imageAnimator.Play("default");

        ContinueStory();
    }


    public void ChooseKnotPathString(string knotName)
    {
        currentStory.ChoosePathString(knotName);
        ContinueStory();
    }

    /**
    *
    * 마우스 클릭 -> 다음 스크립트 실행
    * 다음 스크립트 없음 -> ExitDialogueMode()
    *
    **/
    private void ContinueStory()
    {
        if (currentStory.canContinue) {

            // Set text for the current dialogue line.
            if (displayLineCoroutine != null) {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            // handle tags
            HandleTags(currentStory.currentTags);
        } else {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(String line)
    {
        dialogueText.text = "";

        // hide items while text is typing
        continueIcon.SetActive(false);

        canContinueToNextLine = false;
        isButtonClicked = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time.
        foreach (char letter in line.ToCharArray()) {
            if (isButtonClicked) {
                dialogueText.text = line.Replace('@', '\n');
                break;
            }

            if (letter == '<' || isAddingRichTextTag) {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>' ) {
                    isAddingRichTextTag = false;
                }
            } else if (letter == '@') {
                dialogueText.text += '\n';
            } else {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        // display choices, if any, for this dialogue line

        canContinueToNextLine = true;
    }
    

    private void HandleTags(List<string> currentTags) 
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) {
                Debug.LogError("[DialogueManager] Tag 가 잘못 기록되었습니다. : " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey) {
                case NAME_TAG:
                    if (tagValue == "none") {
                        nameFrame.SetActive(false);
                    } else {
                        nameFrame.SetActive(true);
                    }
                    nameText.text = tagValue;
                    break;
                case IMAGE_TAG:
                    try
                    {
                        imageAnimator.Play(tagValue);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogWarning("Wrong tag value in animator : " + tagValue);
                    }
                    
                    break;
                default:
                    Debug.LogWarning("해당 Tag 는 존재하지 않습니다. : " + tagKey);
                    break;
            }
        }
    }


    
    // 대화 끝
    private void ExitDialogueMode() 
    {
        // yield return new WaitForSeconds(0.2f);

        // dialogueVariables.StopListening(currentStory);
        dialogueIsPlaying = false;
        dialogueCanvas.SetActive(false);
        dialogueText.text = "";
        
        menuManager.ChangePlayerInputSetting(true);
        
        // goAreaButton.gameObject.SetActive(true);

        // 대화 저장
        // dialogueVariables.SaveVariables();
        
    }

    // Click choice button / -1 : from external function
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine) {
            touchAreaButton.gameObject.SetActive(true);
            if (choiceIndex != -1) {
                currentStory.ChooseChoiceIndex(choiceIndex);
                ContinueStory();
            }
        }

    }

}
