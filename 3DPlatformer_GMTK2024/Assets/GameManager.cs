using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //TODO: will need to refactor this at some point.
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;

    [Header("Time Slow")]
    public float timeSlowStrength = 0.05f;
    public float timeSlowDuration = 1f;

    [SerializeField]
    private string m_SceneName;

    [Header("FMOD Music")]
    public FMOD.Studio.EventInstance menuMusic;
    public FMOD.Studio.EventInstance gameplayMusic;
    public FMOD.Studio.EventInstance Ambience;

    //Honestly forgot why I wanted to validate the main scene...
    //keep this in unless it causes issues for your editor, it won't be in the game build anyway.

#if UNITY_EDITOR
    public UnityEditor.SceneAsset SceneAsset;
    private void OnValidate()
    {
        if (SceneAsset != null)
        {
            m_SceneName = SceneAsset.name;
        }
    }
#endif

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void Update()
    {
        //Checking Scenes
        if (SceneManager.GetActiveScene().name == "Cutscene Begin")
        {
            StartCoroutine(StartGameScene());
        }
    }
    IEnumerator StartGameScene()
    {
        Debug.Log("Start Game Scene in 105f");
        yield return new WaitForSeconds(110f);
        //go to the game scene
        Debug.Log("GO");
        SceneChange(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ChangedActiveScene(Scene arg1, Scene arg2)
    {

        Invoke("MusicSelect", .1f);

    }

    private void MusicSelect()
    {
        Debug.Log("Music Select");
        FMODUnity.RuntimeManager.GetBus("bus:/").stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameplayMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music Loop");

            Debug.Log("Gameplay Select");
            gameplayMusic.setParameterByName("Grow", 0.5f);
            gameplayMusic.start();
            gameplayMusic.release();

            Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Forest Ambience");
            Ambience.start();
            Ambience.release();
        }
        else
        {
            menuMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Title Music");

            menuMusic.start();
            menuMusic.release();
        }

    }
    #region GameWorld
    public void BulletTime()
    {
        Time.timeScale = timeSlowStrength;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void StopTime()
    {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public IEnumerator SceneChangeAsyncWDelay(string sceneName, float Time)
    {
        Debug.Log("Called");
        yield return new WaitForSeconds(Time);

        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().SaveGame();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
        Debug.Log("New scene loaded");

        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
    }
    public void SceneChange(string sceneName, float Time)
    {
        StartCoroutine(SceneChangeAsyncWDelay(sceneName, Time));
    }
    public void SceneChange(string sceneName)
    {
        StartCoroutine(SceneChangeAsync(sceneName));
    }

    public void SceneChange(int sceneID)
    {
        StartCoroutine(SceneChangeAsync(sceneID));
    }

    public IEnumerator SceneChangeAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        

        if(loadingScreen == null)
        {
            while (!operation.isDone)
            {
                yield return null;
            }
            yield break;
        }

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;      
        }

        if(loadingScreen)
            loadingScreen.SetActive(false);
        //GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>().LoadGame();
    }
    public IEnumerator SceneChangeAsync(string sceneName)
    {
        Debug.Log("new scene");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        if (loadingScreen == null)
        {
            while (!operation.isDone)
            {
                yield return null;
            }
            yield break;
        }

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
        Debug.Log("new scene loaded");
    }

    internal float WrapEulerAngles(float rotation)
    {
        rotation %= 360;
        if (rotation >= 180)
            return -360;
        return rotation;
    }
    public float UnwrapEulerAngles(float rotation)
    {
        if (rotation >= 0)
            return rotation;

        rotation = -rotation % 360;
        return 360 - rotation;
    }
    public void ScenePreLoad()
    {
        throw new NotImplementedException();
    }

    public void ScenePostLoad()
    {
        throw new NotImplementedException();
    }
#endregion

}
