using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvButton : MonoBehaviour
{
    GameObject nextLvButton;
    LevelManager lvManager;
    LevelData lvData;

    GameObject[] lvCompleteObj;
    bool viewLevel = true;
    bool lvCompleteTrigger = true;
    [SerializeField] Animator toggleUIAnim;
    [SerializeField] Animator levelCompleteAnim;
    [SerializeField] Animator fadeAnim;
    [SerializeField] GameObject[] lvCompleteCleanup;

    //bonus indicator
    [SerializeField] GameObject bonusIndicator;
    [SerializeField] GameObject bonusGlow;
    BonusPoints bonus;
    //flag for if a bonus is available
    [SerializeField] bool BonusAvailable = false;
    [SerializeField] GameObject bonusAvailableObj;
    //bonus already acquired object
    [SerializeField] GameObject bonusPrevAcquired;

    //flag for finished level
    bool levelFinish;
    //flag for showing lv complete screen
    bool showLvComp;
    //flag for cutscene mode (only lv 21)
    bool cutsceneMode = false;

    //audio for button
    AudioSource buttonAudio;

    // Start is called before the first frame update
    void Start()
    {
        nextLvButton = GameObject.FindWithTag("debug_NextLv");
        lvManager = GameObject.FindObjectOfType<LevelManager>();
        lvData = GameObject.Find("LevelData").GetComponent<LevelData>();
        lvCompleteObj = GameObject.FindGameObjectsWithTag("LvComplete");
        buttonAudio = GetComponent<AudioSource>();

        bonus = GameObject.Find("BonusPoints").GetComponent<BonusPoints>();

        levelFinish = false;
        showLvComp = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if level is completed
        if (lvManager.GoalMet() && lvManager.CheckNoPossibleMoves() && !levelFinish)
        {
            levelFinish = true;

            Invoke("DelayedParticles", 1.0f);
            Invoke("CompleteLevel", 2.5f);
        }
        else if (cutsceneMode)
        {

        }
        else if (showLvComp)
        {
            CompleteLevel();
            //Invoke("CompleteLevel", 1.0f);
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

        GameObject fadeObj = GameObject.Find("FadeBG");
        if (fadeObj != null)
        {
            fadeAnim = fadeObj.GetComponent<Animator>();
        }
        if (fadeAnim != null)
        {
            fadeAnim.SetBool("ShowLevel", false);
        }
        Invoke("NextSceneHelper", 2f);
    }

    void NextSceneHelper()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void DelayedParticles()
    {
        LevelCompleteCleanup();
        if (lvManager != null) { lvManager.playAllLvCompParticles(); }
    }

    void LevelCompleteCleanup()
    {
        //hides ui
        if (toggleUIAnim != null)
        {
            toggleUIAnim.SetBool("LevelComplete", true);
        }
        GameObject playerHand = GameObject.Find("PlayerHandArea");
        Animator playerHandAnim = null;
        if (playerHand != null) { playerHandAnim = playerHand.GetComponent<Animator>(); }
        if (playerHandAnim != null) { playerHandAnim.SetBool("LevelComplete", true); }

        foreach(GameObject obj in lvCompleteCleanup)
        {
            Destroy(obj);
        }
    }

    void CompleteLevel()
    {
        showLvComp = true;

        // Destroy clutter on level complete
        //TODO adjust so the ui elements move out instead of disappearing
        //NOTE behaviour moved to DelatedParticles()
        /*
        GameObject[] clutter = GameObject.FindGameObjectsWithTag("DestroyOnLvComp");
        foreach (GameObject obj in clutter)
        {
            Destroy(obj);
        }
        */

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

        //shows interface with options
        //TODO adjust to have a slide in
        
        if(nextLvButton != null)
        {
            nextLvButton.SetActive(true);
        }
        
        if(levelCompleteAnim != null)
        {
            levelCompleteAnim.SetBool("LevelComplete", true);
        }

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

        //show bonus indicator if bonus is met
        if (bonus.CheckBonus(lvManager.GetLevelNum()) && bonusPrevAcquired != null)
        {
            bonusAvailableObj.SetActive(false);
            bonusIndicator.SetActive(true);
        }
        else if (lvManager.BonusMet())
        {
            bonusAvailableObj.SetActive(false);
            bonusIndicator.SetActive(true);
            bonusGlow.transform.Rotate(new Vector3(0f, 0f, 150f * Time.deltaTime));

            bonus.LevelBonus(lvManager.GetLevelNum());
        }
        else
        {
            bonusIndicator.SetActive(false);
            if (BonusAvailable)
            {
                bonusAvailableObj.SetActive(true);
            }
            else
            {
                bonusAvailableObj.SetActive(false);
            }
        }

        //
    }

    public void HideLvComplete()
    {
        buttonAudio.Play();
        if (viewLevel)
        {
            viewLevel = false;
        }
        else
        {
            viewLevel = true;
        }
    }

    public void EnterCutscene()
    {
        Debug.Log("NextLvButton Entering Cutscene");

        cutsceneMode = true;
    }
}
