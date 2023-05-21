using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomPlantData : MonoBehaviour
{
    //Orchid data
    int orchid_row, orchid_col, orchid_val;

    void Start()
    {
        orchid_row = 0;
        orchid_col = 0;
        orchid_val = 10;
    }

    //called when the mom orchid is planted
    //saves the position of the orchid and returns the current value of the orchid
    public int plantOrchid(int row_num, int col_num)
    {
        orchid_row = row_num;
        orchid_col = col_num;
        return orchid_val;
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
