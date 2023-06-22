using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSaveData : MonoBehaviour
{
    //menu asking player if they want to reset save data
    [SerializeField] GameObject resetMenu;
    DataPersistenceManager saveManager;

    private void Update()
    {
        saveManager = GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
    }

    public void ToggleResetSaveMenu()
    {
        if(resetMenu != null)
        {
            if(resetMenu.activeSelf)
            {
                resetMenu.SetActive(false);
            }
            else
            {
                resetMenu.SetActive(true);
            }
        }
    }

    public void ResetConfirm()
    {
        //Debug.Log("Attempting to reset savedata");
        if(saveManager != null)
        {
            //Debug.Log("saveManager found");
            saveManager.ResetGame();
            //saveManager.LoadGame();
        }
        ToggleResetSaveMenu();
    }
}
