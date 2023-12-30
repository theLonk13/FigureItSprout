using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HintButton : MonoBehaviour
{
    HintTracker hintTracker;
    [SerializeField] Image hintButton;
    [SerializeField] Button enableButton; //used to disable button functionality
    [SerializeField] HoverUI hoverScript; //used to disable hovering animations when hints are disabled
    [SerializeField] LevelManager lvMan;
    //general hint info
    [SerializeField] int numHints; //total number of available hints for this level
    [SerializeField] int failTolerance; //number of level failures before the hint button is shown

    //audio for button click
    AudioSource buttonAudio;
    // text for showing number of hints remaining
    [SerializeField] Image numHintsDisplay;
    [SerializeField] TextMeshProUGUI displayNumHints;
    bool playerPlanted = false;
    int hintsAccepted = 0;

    //hint info
    //These should be in order, i.e. hint 1 should be the first plant down and hint 2 should be the second plant
    // hint 1 info
    [SerializeField] GameObject plant1;
    [SerializeField] int row1;
    [SerializeField] int col1;

    //hint 2 info
    [SerializeField] GameObject plant2;
    [SerializeField] int row2;
    [SerializeField] int col2;

    //hint 3 info
    [SerializeField] GameObject plant3;
    [SerializeField] int row3;
    [SerializeField] int col3;

    // Start is called before the first frame update
    void Awake()
    {
        hintTracker = GameObject.Find("HintTracker").GetComponent<HintTracker>();
        buttonAudio = GetComponent<AudioSource>();
        //Invoke("ShowHints", .2f);
        //ShowHintButton();
    }

    // Update is called once per frame
    void Update()
    {
        ShowHintButton();
        //ShowHints();
    }

    public void ShowHintButton()
    {
        if((hintTracker.getHintsAccepted() >= numHints) || hintsAccepted >= numHints || playerPlanted)//|| (hintTracker.getLocalFails() <= failTolerance))
        {
            hintButton.color = new Color(68f / 255f, 68f / 255f, 68f / 255f, 255f / 255f);
            numHintsDisplay.color = new Color(68f / 255f, 68f / 255f, 68f / 255f, 255f / 255f);
            //displayNumHints.color = new Color(68f / 255f, 68f / 255f, 68f / 255f, 255f / 255f);
            enableButton.enabled = false;
            //hoverScript.enabled = false;
        }
        else
        {
            hintButton.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            numHintsDisplay.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            //displayNumHints.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            enableButton.enabled = true;
            //hoverScript.enabled = true;
        }
        displayNumHints.text = "" + (Mathf.Max(0, numHints - hintsAccepted));
    }

    public void AcceptHint()
    {
        buttonAudio.Play();
        hintsAccepted++;
        if(hintsAccepted == 1)
        {
            ShowHint1();
        }else if(hintsAccepted == 2)
        {
            ShowHint2();
        }else if(hintsAccepted == 3)
        {
            ShowHint3();
        }

        playerPlanted = false;
        //hintTracker.AcceptHint(); //used in old hint system
        //Destroy(hintButton);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ShowHints()
    {
        ShowHint1();
        Invoke("ShowHint2", .5f);
        //ShowHint2();
    }

    void ShowHint1()
    {
        //Debug.LogError("Showing Hint 1");
        if (hintsAccepted >= 1 && plant1 != null)
        {
            LevelTile tile = lvMan.findTile(row1, col1);
            tile.plantPlant(plant1.GetComponent<PlantCard>().getPlantID());
            Destroy(plant1);
        }
    }

    void ShowHint2()
    {
        //Debug.LogError("Showing Hint 2");
        if (hintsAccepted >= 2 && plant2 != null)
        {
            LevelTile tile = lvMan.findTile(row2, col2);
            tile.plantPlant(plant2.GetComponent<PlantCard>().getPlantID());
            Destroy(plant2);
        }
    }

    void ShowHint3()
    {
        //Debug.LogError("Showing Hint 2");
        if (hintsAccepted >= 3 && plant3 != null)
        {
            LevelTile tile = lvMan.findTile(row3, col3);
            tile.plantPlant(plant3.GetComponent<PlantCard>().getPlantID());
            Destroy(plant3);
        }
    }

    public void PlayerPlanted()
    {
        playerPlanted = true;
    }
}
