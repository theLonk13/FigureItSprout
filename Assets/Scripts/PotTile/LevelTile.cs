using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTile : MonoBehaviour
{
    public int row, col, curr_score;
    int plantType, turnCounter;

    //GameObject plant;       //tracks the plant currently planted on this tile
    LevelManager levelManager;

    //persistent script that holds the potted sprites
    PottedSpriteInfo potted_sprites;

    //Image rendering this pot
    Image thisImage;

    //Indicator for shy plant activation
    int shy_toggle = -1;
    [SerializeField] GameObject shy_indicator;

    void Start()
    {
        plantType = -1;
        //curr_score = 0;
        turnCounter = 0;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        potted_sprites = GameObject.FindWithTag("PottedSprite").GetComponent<PottedSpriteInfo>();
        thisImage = this.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        displayShy();
    }

    public void plantPlant(int plantID)
    {
        plantType = plantID;
        //TODO: initialize the current score of this tile to the base score of the plant
        //(I think this is already done in plant actions)
        //TODO: maybe do plant actions here? Currently doing them in LevelManager
        levelManager.plantAction(row, col, plantID);
        thisImage.sprite = potted_sprites.get_potted_sprite(plantID);
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
        if (shy_toggle > 0)
        {
            shy_indicator.SetActive(true);
            thisImage.sprite = potted_sprites.trigger_plant(plantType);
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
}
