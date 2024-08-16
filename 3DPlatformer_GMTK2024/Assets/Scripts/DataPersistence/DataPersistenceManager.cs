using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;
    public GameData gameData;

    public static DataPersistenceManager instance { get; private set; }

    private List<IDataPersistence> dataPersistenceObjects;
    
    private FileDataHandler dataHandler;

    private void Start()
    {
        try
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void Awake()
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Debug.LogError("More than one instance of Data Persistence Manager Found!");
            Destroy(this.gameObject);
        }

        instance = this;
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void SaveGame()
    {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        //pass the data to other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
      //  Debug.Log("Enemies Killed save: " + gameData.deathCount);
        //save that data to a file using the data handler
        dataHandler.Save(gameData);
    }
    public void LoadGame()
    {
        if(dataHandler!=null)
            gameData = dataHandler.Load();

        // if no data can be found, init to NewGame
        if (this.gameData == null)
        {
            Debug.Log("No Save Data was found, Initialising to New Game");
            NewGame();
        }

        dataPersistenceObjects = FindAllDataPersistenceObjects();
        // TODO - push loaded data to scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Enemies Killed load: " + gameData.deathCount);

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
