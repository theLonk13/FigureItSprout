using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    //holds all gameobjects that are the icons for the different levels
    GameObject[] levels;
    LevelData levelDataScript;
    BonusPoints bonusPointsScript;
    int[] levelData;
    int[] actSkipData;
    int[] bonusPoints;

    [SerializeField] ActSwitch actSwitch;

    [SerializeField] bool playTestMode = false;

    // Start is called before the first frame update
    void Awake()
    {
        levels = GameObject.FindGameObjectsWithTag("LevelSelector");
        GameObject persistent_lvData = GameObject.Find("LevelData");
        levelDataScript = persistent_lvData.GetComponent<LevelData>();
        bonusPointsScript = GameObject.Find("BonusPoints").GetComponent<BonusPoints>();
        bonusPoints = bonusPointsScript.GetBonusStarData();

        Debug.Log(levelDataScript.getLastLevel() + "");
        if(levelDataScript != null && levelDataScript.getLastLevel() < 11)
        {
            actSwitch.changeAct(1);
        }else if(levelDataScript.getLastLevel() < 22)
        {
            actSwitch.changeAct(2);
        }else if( levelDataScript.getLastLevel() <= 31)
        {
            actSwitch.changeAct(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        levelData = levelDataScript.get_level_data();
        actSkipData = levelDataScript.GetActSkips();
        if (!playTestMode) { showLevels(); }
        debug_ShowActSkipData();
    }

    void showLevels()
    {
        foreach(GameObject level in levels)
        {
            LevelSelectIcon selectScript = level.GetComponent<LevelSelectIcon>();
            if (selectScript != null)
            {
                int lvNum = selectScript.getLvNum();
                if (levelData[lvNum - 1] != 0 || (GetLevelActNum(lvNum) != -1 && actSkipData[GetLevelActNum(lvNum) - 1] != 0))
                {
                    level.SetActive(true);
                    if (bonusPoints[lvNum - 1] != 0)
                    {
                        selectScript.ShowBonusAcquired();
                    }
                    else
                    {
                        selectScript.ShowBonusNotAcquired();
                    }
                }
                else
                {
                    level.SetActive(false);
                }
            }
        }
    }

    int GetLevelActNum(int lvNum)
    {
        if(lvNum > 0 && lvNum <= 10)
        {
            return 1;
        }else if(lvNum <= 21)
        {
            return 2;
        }else if(lvNum <= 31)
        {
            return 3;
        }
        return -1;
    }

    void debug_ShowActSkipData()
    {
        string actSkipString = "";
        foreach(int act in actSkipData)
        {
            actSkipString += act + " ";
        }
        Debug.Log(actSkipString);
    }
}
