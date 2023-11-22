using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public int[] UnlockedLevels;
    public int[] BonusStars;
    public int[] LevelResets;
    public int[] ActSkips;

    public SaveData()
    {
        //LevelData levelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        //int numLevels = levelData.getNumLevels();
        int numLevels = 31;

        this.UnlockedLevels = new int[numLevels];
        this.BonusStars = new int[numLevels];
        this.LevelResets = new int[numLevels];
        this.ActSkips = new int[3];
    }
}
