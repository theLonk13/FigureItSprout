using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour, IDataPersistance
{
    [SerializeField] int numLevels = 1;

    //array tracks which levels the player has seen
    int[] level_tracker;
    int[] level_resets;

    //last level player was on, used to flip to correct act when moving back to level select
    int last_level;

    // Start is called before the first frame update
    void Start()
    {
        last_level = -1;

        if(level_tracker == null)
        {
            level_tracker = new int[numLevels];
        }

        level_resets = new int[numLevels];
        //level_tracker[0] = 1;
    }

    void Update()
    {
        debug_printLvData();
        debug_printLvResets();
    }

    public void LoadData(SaveData saveData)
    {
        this.level_tracker = saveData.UnlockedLevels;
    }

    public void SaveData(ref SaveData saveData)
    {
        saveData.UnlockedLevels = this.level_tracker;
    }

    //marks a level as having been visited and for its icon to show in level select
    public void ActivateLevel(int level)
    {
        last_level = level;
        if (level > 0 && level <= numLevels)
        {
            level_tracker[level - 1] = 1;
        }
    }

    public int[] get_level_data()
    {
        return level_tracker;
    }

    void debug_printLvData()
    {
        string unlocks = "";
        foreach (int unlock in level_tracker)
        {
            unlocks += unlock + " ";
        }
        Debug.Log("Level Unlocks:\n" + unlocks);
    }

    public void completeLv(int lvNum)
    {
        lvNum--;
        if(lvNum >= 0 && lvNum < numLevels)
        {
            level_resets[lvNum] = 1;
        }
    }

    public void resetLv(int lvNum)
    {
        lvNum--;
        if(lvNum >= 0 && lvNum < numLevels)
        {
            level_resets[lvNum] = level_resets[lvNum] + 1;
        }
    }

    public int getLvResets(int lvNum)
    {
        return level_resets[lvNum - 1];
    }

    void debug_printLvResets()
    {
        string resets = "";
        foreach (int reset in level_resets)
        {
            resets += reset + " ";
        }
        Debug.Log("Level Resets:\n" + resets);
    }

    public int getNumLevels()
    {
        return numLevels;
    }
    
    public int getLastLevel()
    {
        return last_level;
    }
}
