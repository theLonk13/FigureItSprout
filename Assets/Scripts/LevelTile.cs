using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    /*
    public LevelTile topLeft;
    public LevelTile topMid;
    public LevelTile topRight;
    public LevelTile midLeft;
    public LevelTile midRight;
    public LevelTile botLeft;
    public LevelTile botMid;
    public LevelTile botRight;
    */

    public int row, col, curr_score;
    int plantType, turnCounter;
    GameObject plant;       //tracks the plant currently planted on this tile


    void Start()
    {
        plantType = -1;
        curr_score = 0;
        turnCounter = 0;
    }

    public void plantPlant(int plantID)
    {
        plantType = plantID;
        //TODO: initialize the current score of this tile to the base score of the plant
        //TODO: maybe do plant actions here? Currently doing them in LevelManager
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

    /*
    public LevelTile getAdj(string pos)
    {
        if(string.equals(pos, "TopLeft"))
        {
            return topLeft;
        }else if(string.equals(pos, "TopMid"))
        {
            return topMid;
        }else if(string.equals(pos, "TopRight"))
        {
            return topRight;
        }else if(string.equals(pos, "MidLeft"))
        {
            return midLeft;
        }else if(string.equals(pos, "MidRight"))
        {
            return midRight;
        }else if(string.equals(pos, "BotLeft"))
        {
            return botLeft;
        }else if(string.equals(pos, "BotMid"))
        {
            return botMid;
        }else if(string.equals(pos, "BotRight"))
        {
            return botRight;
        }
        else
        {
            return null;
        }
    }
    */
}
