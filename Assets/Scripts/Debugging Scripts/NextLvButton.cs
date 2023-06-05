using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvButton : MonoBehaviour
{
    GameObject nextLvButton;
    LevelManager lvManager;

    GameObject[] lvCompleteObj;
    bool viewLevel = true;
    bool lvCompleteTrigger = true;

    //bonus indicator
    [SerializeField] GameObject bonusIndicator;
    [SerializeField] GameObject bonusGlow;
    BonusPoints bonus;

    //flag for finished level
    bool levelFinish;
    //flag for showing lv complete screen
    bool showLvComp;

    // Start is called before the first frame update
    void Start()
    {
        nextLvButton = GameObject.FindWithTag("debug_NextLv");
        lvManager = GameObject.FindObjectOfType<LevelManager>();
        lvCompleteObj = GameObject.FindGameObjectsWithTag("LvComplete");

        levelFinish = false;
        showLvComp = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if level is completed
        if (lvManager.GoalMet() && !levelFinish)
        {
            levelFinish = true;

            Invoke("CompleteLevel", 1);            
        }
        else if (showLvComp)
        {
            
        }
        else
        {
            nextLvButton.SetActive(false);
        }
    }

    public void nextScene()
    {
        MomPlantData mom_plant = GameObject.Find("MomPlantData").GetComponent<MomPlantData>();
        if(mom_plant != null)
        {
            mom_plant.saveOrchid();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void CompleteLevel()
    {
        showLvComp = true;

        // Destroy clutter on level complete
        GameObject[] clutter = GameObject.FindGameObjectsWithTag("DestroyOnLvComp");
        foreach (GameObject obj in clutter)
        {
            Destroy(obj);
        }

        //make sure the lv complete screen objects exist
        if (lvCompleteTrigger)
        {
            lvCompleteTrigger = false;
            HideLvComplete();
        }

        //activate lv complete screen
        LevelData lvData = GameObject.Find("LevelData").GetComponent<LevelData>();
        if (lvData != null)
        {
            lvData.completeLv(lvManager.GetLevelNum());
        }
        nextLvButton.SetActive(true);

        foreach (GameObject obj in lvCompleteObj)
        {
            if (viewLevel)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }

        //show bonus indicator
        if (lvManager.BonusMet())
        {
            bonusIndicator.SetActive(true);
            bonusGlow.transform.Rotate(0f, 0f, 1f * Time.deltaTime);

            bonus = GameObject.Find("BonusPoints").GetComponent<BonusPoints>();
            bonus.LevelBonus(lvManager.GetLevelNum());
        }
        else
        {
            bonusIndicator.SetActive(false);
        }
    }

    public void HideLvComplete()
    {
        if (viewLevel)
        {
            viewLevel = false;
        }
        else
        {
            viewLevel = true;
        }
    }
}
