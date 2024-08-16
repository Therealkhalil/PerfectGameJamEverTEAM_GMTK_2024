using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log("Loading Data from: " + fullPath);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try 
            {
                //Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } 
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to Load data to file at path: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save (GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log("Saving Data from: " + fullPath);
        try
        {
            //Create directory file will be written if it doesn't exist already
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize the C# game Data Object to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialized data to file
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
                
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file at path: " + fullPath + "\n" + e);
        }
    }
}
