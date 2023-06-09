using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    //holds all gameobjects that are the icons for the different levels
    GameObject[] levels;
    LevelData levelDataScript;
    int[] levelData;

    [SerializeField] ActSwitch actSwitch;

    // Start is called before the first frame update
    void Awake()
    {
        levels = GameObject.FindGameObjectsWithTag("LevelSelector");
        GameObject persistent_lvData = GameObject.Find("LevelData");
        levelDataScript = persistent_lvData.GetComponent<LevelData>();

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
        //showLevels();
    }

    void showLevels()
    {
        foreach(GameObject level in levels)
        {
            LevelSelectIcon selectScript = level.GetComponent<LevelSelectIcon>();
            if (selectScript != null)
            {
                int lvNum = selectScript.getLvNum();
                if (levelData[lvNum - 1] != 0)
                {
                    level.SetActive(true);
                }
                else
                {
                    level.SetActive(false);
                }
            }
        }
    }
}
