using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class LevelData : MonoBehaviour, IDataPersistance
{
    [SerializeField] int numLevels = 1;

    //array tracks which levels the player has seen
    int[] level_tracker;
    int[] level_resets;

    int[] act_skips = new int[3];

    //last level player was on, used to flip to correct act when moving back to level select
    int last_level;

    [SerializeField] int Act1CheevoLv;
    [SerializeField] int Act2CheevoLv;

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
        //debug_printLvData();
        //debug_printLvResets();
    }

    public void LoadData(SaveData saveData)
    {
        this.level_tracker = saveData.UnlockedLevels;
        this.level_resets = saveData.LevelResets;
        this.act_skips = saveData.ActSkips;
    }

    public void SaveData(ref SaveData saveData)
    {
        saveData.UnlockedLevels = this.level_tracker;
        saveData.LevelResets = this.level_resets;
        saveData.ActSkips = this.act_skips;
    }

    //marks a level as having been visited and for its icon to show in level select
    public void ActivateLevel(int level)
    {
        last_level = level;
        if (level > 0 && level <= numLevels)
        {
            level_tracker[level - 1] = 1;
        }
        //CheckAchievement();
    }

    //Check Steam Achievements for completing acts
    void CheckAchievement()
    {
        if (level_tracker[Act1CheevoLv - 1] == 1)
        {
            //Check and set "COMPLETE ACT 1" achievement here
        }

        if (level_tracker[Act2CheevoLv - 1] == 1)
        {
            //Check and set "COMPLETE ACT 2" achievement here
        }

        if (level_tracker[numLevels - 1] == 1)
        {
            //Check and set "COMPLETE THE WHOLE GAME" achievement here
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
        //Debug.Log("Level " + lvNum + " completed");
        lvNum--;
        if(lvNum >= 0 && lvNum < numLevels)
        {
            level_resets[lvNum] = 1;
        }

        //activat the next level on level select screen
        if(lvNum + 1 < numLevels)
        {
            level_tracker[lvNum + 1] = 1;
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
        Debug.Log("" + lvNum + " " + level_resets.Length);
        return level_resets[lvNum - 1];
    }

    void debug_printLvResets()
    {
        string resets = "";
        foreach (int reset in level_resets)
        {
            resets += reset + " ";
        }
        //Debug.Log("Level Resets:\n" + resets);
    }

    public int getNumLevels()
    {
        return numLevels;
    }
    
    public int getLastLevel()
    {
        return last_level;
    }

    public void UnlockAct(int actNum)
    {
        act_skips[actNum - 1] = 1;
    }

    public int[] GetActSkips()
    {
        return act_skips;
    }
   
}
