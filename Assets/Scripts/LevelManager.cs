using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    //performs an action corresponding to the plant on a tile
    public void plantAction(int row_num, int col_num, int plantID)
    {
        switch (plantID)
        {
            case 1:
                SpecialPlant(row_num, col_num);
                break;
            case 2:

                break;
            default:
                Debug.Log("What are you doing with your life?");
                break;
        }
    }

    //performs an action corresponding to the plant on row of index row_num except for the one in col_num
    /*
    public void plantActionRow(int row_num, int col_num, int plantID)
    {        
        foreach(GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if(tiledata.row == row_num && tiledata.col != col_num)
            {
                plantAction(row_num, col_num, plantID);
            }
        }
    }
    */

    //TODO IMPLEMENT ALL PLANT ACTIONS HERE
    //ID 1: the special mom plant
    void SpecialPlant(int row_num, int col_num)
    {
        foreach(GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if(tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                tiledata.curr_score++;
            }
        }
    }

    void Sage(int row_num, int col_num)
    {
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                tiledata.curr_score++;
            }
        }
    }

    void Parsley(int row_num, int col_num)
    {
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                tiledata.curr_score += 2;
            }
        }
    }

    void Cilantro(int row_num, int col_num)
    {
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                tiledata.curr_score += 3;
            }
        }
    }

    void Lemongrass(int row_num, int col_num)
    {
        int total_points = 0;
        LevelTile lemonData;
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col != col_num)
            {
                total_points++;
            }else if(tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                lemonData = tiledata;
            }
        }
        lemonData.curr_score = 1 + total_points;
    }
}
