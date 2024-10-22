using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomPlantData : MonoBehaviour
{
    //Orchid data
    int orchid_row, orchid_col, orchid_val, orchid_val18, orchid_val19, orchid_val20, orchid_val21, orchid_val22;

    void Start()
    {
        orchid_row = 0;
        orchid_col = 0;
        orchid_val = 10;
        orchid_val18 = 10;
        orchid_val19 = 9;
        orchid_val20 = 7;
        orchid_val21 = 4;
        orchid_val22 = 2;
    }

    //called when the mom orchid is planted
    //saves the position of the orchid and returns the current value of the orchid
    public int plantOrchid(int row_num, int col_num, int lvNum)
    {
        orchid_row = row_num;
        orchid_col = col_num;
        switch (lvNum)
        {
            case 18:
                return orchid_val18;
            case 19:
                return orchid_val19;
            case 20:
                return orchid_val20;
            case 21:
                return orchid_val21;
            case 22:
                return orchid_val22;
            default:
                return -1;
        }
    }

    //saves the current value of the orchid
    public void saveOrchid()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == orchid_row && tiledata.col == orchid_col && tiledata.getPlantType() == 16)
            {
                orchid_val = tiledata.curr_score;
            }
        }
    }
}
