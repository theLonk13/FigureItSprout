using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour, IDataPersistance
{
    [SerializeField] LevelData levelData;
    int[] levelStarTracker;

    //milestones for bonus stars
    [SerializeField] int BonusStarMilestone1 = 2;
    [SerializeField] int BonusStarMilestone2 = 3;
    [SerializeField] int MaxBonusStars = 4;

    // Start is called before the first frame update
    void Start()
    {
        if(levelStarTracker == null)
        {
            levelStarTracker = new int[levelData.getNumLevels()];
        }
    }

    public void LevelBonus(int lvNum)
    {
        levelStarTracker[lvNum - 1] = 1;
    }

    //Check if player has enough stars for achievement
    void CheckBonusAchievement()
    {
        int playerStars = GetStarCount();
        if(playerStars >= MaxBonusStars)
        {

        }else if(playerStars >= BonusStarMilestone2)
        {

        }else if(playerStars >= BonusStarMilestone1)
        {
            
        }else if(playerStars >= 1)
        {

        }
    }

    public int[] GetBonusStarData()
    {
        return levelStarTracker;
    }

    public bool CheckBonus(int lvNum)
    {
        return levelStarTracker[lvNum - 1] > 0;
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

    public void LoadData(SaveData saveData)
    {
        levelStarTracker = saveData.BonusStars;
    }

    public void SaveData(ref SaveData saveData)
    {
        saveData.BonusStars = levelStarTracker;
    }
}
