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

    public SaveData Load()
    {
        //create file path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //load serialized data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize data to C# obj
                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load from file: " + fullPath + "\n" + e.ToString());
            }
        }
        return loadedData;
    }

    public void Save(SaveData saveData)
    {
        //create file path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //create directory for the file if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize game data into Json
            string dataToStore = JsonUtility.ToJson(saveData, true);

            //write serialized data to a file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while trying to save to file: " + fullPath + "\n" + e.ToString());
        }
    }
}
