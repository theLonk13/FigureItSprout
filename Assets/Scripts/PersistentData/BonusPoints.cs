using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    int[] levelStarTracker;

    // Start is called before the first frame update
    void Start()
    {
        levelStarTracker = new int[levelData.getNumLevels()];
    }

    public void LevelBonus(int lvNum)
    {
        levelStarTracker[lvNum - 1] = 1;
    }

    public int GetStarCount()
    {
        int count = 0;
        foreach(int level in levelStarTracker)
        {
            if(level > 0) { count++; }
        }
        return count;
    }
}
