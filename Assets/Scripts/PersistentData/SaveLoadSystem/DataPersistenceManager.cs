using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string filename;


    private SaveData saveData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance {  get; private set; }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, filename);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one DataPersistanceManager found.");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void NewGame()
    {
        this.saveData = new SaveData();
    }

    public void LoadGame()
    {
        //load saved data via file data handler (not yet created)
        this.saveData = dataHandler.Load();

        //if no data is found, initialize new game
        if(saveData == null)
        {
            Debug.Log("No save data found, initializing new game");
            NewGame();
        }
        else
        {
            Debug.Log("Save data found, loading level unlocks");
            string unlocks = "";
            foreach (int unlock in saveData.UnlockedLevels)
            {
                unlocks += unlock + " ";
            }
            Debug.Log("Loaded Level Unlocks:\n" + unlocks);

            string stars = "";
            foreach (int starUnlock in saveData.BonusStars)
            {
                stars += starUnlock + " ";
            }
            Debug.Log("Loaded Bonus Stars:\n" + stars);
        }

        //push loaded data to scripts that need it
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(saveData);
        }
    }

    public void SaveGame()
    {
        //pass data to other scripts so they can update it
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref saveData);
        }

        //debugging
        string unlocks = "";
        foreach (int unlock in saveData.UnlockedLevels)
        {
            unlocks += unlock + " ";
        }
        Debug.Log("Saving Level Unlocks:\n" + unlocks);


        //save data to a file using file data handler (not yet created)
        dataHandler.Save(saveData);
    }

    public void ResetGame()
    {
        NewGame();
        dataHandler.Save(saveData);
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
