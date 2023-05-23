using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] int numLevels = 1;

    //array tracks which levels the player has seen
    int[] level_tracker;
    int[] level_resets;

    // Start is called before the first frame update
    void Start()
    {
        level_tracker = new int[numLevels];
        level_resets = new int[numLevels];
        level_tracker[0] = 1;
    }

    void Update()
    {
        debug_printLvData();
        debug_printLvResets();
    }

    //marks a level as having been visited and for its icon to show in level select
    public void ActivateLevel(int level)
    {
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
}
