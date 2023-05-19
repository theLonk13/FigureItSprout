using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] int numLevels = 1;

    //array tracks which levels the player has seen
    int[] level_tracker;

    // Start is called before the first frame update
    void Start()
    {
        level_tracker = new int[numLevels];
        level_tracker[0] = 1;
    }

    void Update()
    {
        debug_printLvData();
    }

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
}
