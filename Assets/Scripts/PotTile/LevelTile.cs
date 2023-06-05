using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTile : MonoBehaviour
{
    public int row, col, curr_score;
    int plantType, turnCounter;

    //GameObject plant;       //tracks the plant currently planted on this tile
    LevelManager levelManager;

    //persistent script that holds the potted sprites
    PottedSpriteInfo potted_sprites;

    //Image rendering this pot
    [SerializeField] GameObject potImageObj;
    RectTransform potImageRect;
    Image potImage;
    //Empty tile image
    Image tileImage;

    //Indicator for shy plant activation
    int shy_toggle = -1;
    [SerializeField] GameObject shy_indicator;

    //Indicator for blossoming plant counter
    [SerializeField] GameObject blossom_indicator;
    [SerializeField] TextMeshProUGUI blossom_text;

    //Score object
    [SerializeField] GameObject scoreDisplay;

    //animator
    Animator animator;


    void Start()
    {
        plantType = -1;
        //curr_score = 0;
        turnCounter = -1;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        tileImage = this.gameObject.GetComponent<Image>();
        potImage = potImageObj.GetComponent<Image>();
        potImageRect = potImageObj.GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(potted_sprites == null)
        {
            LoadSpriteInfo();
        }

        displayShy();
        displayBlossom();
        if(plantType > 0)
        {
            potImageObj.SetActive(true);
            scoreDisplay.SetActive(true);
        }
        else
        {
            potImageObj.SetActive(false);
            scoreDisplay.SetActive(false);
        }

        fallingPot();
    }

    void LoadSpriteInfo()
    {
        //load correct sprite data for time of day
        int timeOfDay = levelManager.GetTimeOfDay();
        if (timeOfDay == 0)
        {
            potted_sprites = GameObject.Find("DayPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        }else if(timeOfDay == 1)
        {
            potted_sprites = GameObject.Find("SunsetPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        }else if(timeOfDay == 2)
        {
            potted_sprites = GameObject.Find("NightPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        }
    }

    public void plantPlant(int plantID)
    {
        plantType = plantID;
        //TODO: initialize the current score of this tile to the base score of the plant
        //(I think this is already done in plant actions)
        //TODO: maybe do plant actions here? Currently doing them in LevelManager
        levelManager.plantAction(row, col, plantID);
        potImage.sprite = potted_sprites.get_potted_sprite(plantID);

        //moving this tile to end of child list
        //*
        int numChildren = this.transform.parent.gameObject.transform.childCount;
        this.transform.SetSiblingIndex(numChildren - 1);
        //*/

        //start pot falling animation
        Color tempColor = potImage.color;
        tempColor.a = Mathf.Min(.5f, 1.0f);
        potImage.color = tempColor;
        potImageRect.localScale = new Vector3(2f, 2f, 1f);

        //set animator to planted
        animator.SetBool("Planted", true);
    }

    //Increments the turn counter of this tile and returns the value of the new turn counter
    public int incCounter()
    {
        turnCounter++;
        return turnCounter;
    }

    //returns plantType
    public int getPlantType()
    {
        return plantType;
    }

    void displayShy()
    {
        shy_indicator.SetActive(false);
        if (plantType != 15) { return; }
        if (shy_toggle > 0)
        {
            shy_indicator.SetActive(true);
        }
        else
        {
            potImage.sprite = potted_sprites.trigger_plant(plantType);
        }
    }

    public void toggleShy()
    {
        shy_toggle = shy_toggle * -1;
    }

    public int getShyToggle()
    {
        return shy_toggle;
    }

    void displayBlossom()
    {
        if(plantType != 17 && plantType != 18) {
            blossom_indicator.SetActive(false); 
            return; 
        }
        blossom_indicator.SetActive(true) ;

        if(plantType == 17) {
            blossom_text.SetText(Mathf.Max(0, 2 - turnCounter) + ""); 
            if(turnCounter >= 2)
            {
                blossom_indicator.SetActive(false);
                potImage.sprite = potted_sprites.bloom_plant(plantType);
            }
        }
        else if(plantType == 18) { 
            blossom_text.SetText(Mathf.Max(0, 3 - turnCounter) + "");
            if (turnCounter >= 3)
            {
                blossom_indicator.SetActive(false);
                potImage.sprite = potted_sprites.bloom_plant(plantType);
            }
        }
    }

    //"animation" for placing a pot down
    void fallingPot()
    {
        if(potImage.color.a < 1f)
        {
            Color tempColor = potImage.color;
            tempColor.a = Mathf.Min(tempColor.a + .01f, 1.0f);
            potImage.color = tempColor;
        }

        potImageRect.localScale = new Vector3(Mathf.Max(potImageRect.localScale.x - .02f, 1f), Mathf.Max(potImageRect.localScale.y - .02f, 1f), 1f);
    }
}
