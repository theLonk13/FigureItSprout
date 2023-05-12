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
    int plantType;
    GameObject plant;       //tracks the plant currently planted on this tile


    void Start()
    {
        plantType = -1;
        curr_score = 0;
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
