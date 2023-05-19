using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    GameObject[] tiles;
    [SerializeField] int max_level_size = 1;

    //score counters
    //[SerializeField] TextMeshPro total_score;
    [SerializeField] TextMeshProUGUI total_score_canvas;
    [SerializeField] TextMeshProUGUI goal_score_canvas;
    [SerializeField] int goal_score = 1;

    //unlocked plant pages
    [SerializeField] int unlock_plant1 = -1;
    [SerializeField] int unlock_plant2 = -1;

    //level number for level select
    [SerializeField] int lvNum;

    int total_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        goal_score_canvas.SetText("Goal Score: " + goal_score);

        unlock_level();
        unlock_pages();

        //This is for debugging, maybe take out
        UpdateScore();
    }

    //performs an action corresponding to the plant on a tile
    //Also used to advance a turn
    //TODO Fill out with the rest of the plants
    public void plantAction(int row_num, int col_num, int plantID)
    {
        Debug.Log("Attempting to plant a plant of ID " + plantID);
        //Check "Setup" plant actions
        checkShame(row_num, col_num);

        //Perform "Active" plant actions
        //TODO Update this with other plants as IDs are finalized
        switch (plantID)
        {
            case 1:
                SpecialPlant(row_num, col_num);
                break;
            case 2:
                Sage(row_num, col_num);
                break;
            case 3:
                Parsley(row_num, col_num);
                break;
            case 4:
                Cilantro(row_num, col_num);
                break;
            case 5:
                Lemongrass(row_num, col_num);
                break;
            case 6:
                Thyme(row_num, col_num);
                break;
            case 7:
                Clover(row_num, col_num);
                break;
            case 8:
                Alfalfa(row_num, col_num);
                break;
            case 9:
                Basil(row_num, col_num);
                break;
            case 10:
                SunSucc(row_num, col_num);
                break;
            case 11:
                Vipergrass(row_num, col_num);
                break;
            case 12:
                Crabgrass(row_num, col_num);
                break;
            default:
                Debug.Log("What are you doing with your life?");
                break;
        }

        //Increment plant counters and perform "Turn Counter" plant actions
        incCounterPlants();

        //Update the score after all plant actions have finished
        UpdateScore();
    }

    //Searches the tiles array for a tile in a specific position
    //Returns the LevelTile script attached to the tile if found, null otherwise
    LevelTile findTile(int row_num, int col_num)
    {
        foreach(GameObject tile in tiles)
        {
            if(tile != null)
            {
                LevelTile tiledata = tile.GetComponent<LevelTile>();
                if(tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
                {
                    return tiledata;
                }
            }
        }
        return null;
    }

    //Totals the score of all plants after the turn is ended
    void UpdateScore()
    {
        total_score = 0;
        foreach(GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            total_score += tiledata.curr_score;
        }
        //total_score.SetText(score + "");
        total_score_canvas.SetText("Total Score: " + total_score);
    }

    //returns score
    public int GetScore()
    {
        return total_score;
    }

    //returns true if the total score of the tiles is at least the goal score, false otherwise
    public bool GoalMet()
    {
        return total_score >= goal_score;
    }

    void unlock_pages()
    {
        UnlockedPlants unlocks = GameObject.Find("PlantUnlocks").GetComponent<UnlockedPlants>();
        unlocks.UnlockPlant(unlock_plant1);
        unlocks.UnlockPlant(unlock_plant2);
    }

    void unlock_level()
    {
        LevelData lv_unlocks = GameObject.Find("LevelData").GetComponent<LevelData>();
        lv_unlocks.ActivateLevel(lvNum);
    }

    //TODO IMPLEMENT ALL PLANT ACTIONS HERE
    //"Active" plant actions: These actions occur on planting the plant
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

    //ID ??: the EPIC special mom plant
    void EpicPlant(int row_num, int col_num)
    {
        LevelTile tiledata = findTile(row_num, col_num);
        if(tiledata != null)
        {
            tiledata.curr_score = 10;
        }
    }

    //ID 2: Sage - Worth 1 pt. No special effect
    void Sage(int row_num, int col_num)
    {
        Debug.Log("Attempting to plant sage");
        LevelTile tiledata = findTile(row_num, col_num);
        if(tiledata != null)
        {
            Debug.Log("Sage planted");
            tiledata.curr_score = 1;
        }

        /*
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                tiledata.curr_score = 1;
            }
        }
        */
    }

    //ID 3: Parsley - Worth 2 pts. No special effect
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

    //ID 4: Cilantro - Worth 3 pts. No special effect
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

    //Id 5: Lemongrass - Worth 1 pt. On plant, gains 1 additional pt for each plant in the same row
    void Lemongrass(int row_num, int col_num)
    {
        int total_points = 0;
        LevelTile lemonData = null;
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col != col_num && tiledata.getPlantType() > 0)
            {
                total_points++;
            }else if(tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                lemonData = tiledata;
            }
        }

        if(lemonData != null)
        {
            lemonData.curr_score = 1 + total_points;
        }
    }

    //ID 6: Thyme - Worth 1 pt. On plant, gains 2 additional pts for each plant in the same row
    void Thyme(int row_num, int col_num)
    {
        int total_points = 0;
        LevelTile thymeData = null;
        foreach (GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            if (tiledata != null && tiledata.row == row_num && tiledata.col != col_num)
            {
                total_points += 2;
            }
            else if (tiledata != null && tiledata.row == row_num && tiledata.col == col_num)
            {
                thymeData = tiledata;
            }
        }

        if (thymeData != null)
        {
            thymeData.curr_score = 1 + total_points;
        }
    }

    //ID 7: Clovers - Worth 0 pts. Multiplies the current point value of plants in the tiles directly above, to the left, to the right and below
    //the tile the clovers were planted in by 2.
    void Clover(int row_num, int col_num)
    {
        LevelTile curr_tile = null;

        //Tile directly above
        curr_tile = findTile(row_num, col_num - 1);
        if(curr_tile != null)
        {
            curr_tile.curr_score *= 2;
        }

        //Tile direct to the left
        curr_tile = findTile(row_num - 1, col_num);
        if(curr_tile != null)
        {
            curr_tile.curr_score *= 2;
        }

        //Tile direct to the right
        curr_tile = findTile(row_num + 1, col_num);
        if(curr_tile != null)
        {
            curr_tile.curr_score *= 2;
        }

        //Tile directly below
        curr_tile = findTile(row_num, col_num + 1);
        if(curr_tile != null)
        {
            curr_tile.curr_score *= 2;
        }
    }

    //ID 8: Alfalfa - Worth 0 pts. Multiplies the current point value of plants in the same column by 2
    //RECODE to a foreach loop that just checks col parameter
    void Alfalfa(int row_num, int col_num)
    {
        LevelTile curr_tile = null;
        for(int i = 0; i < max_level_size; i++)
        {
            curr_tile = findTile(row_num, i);
            if(curr_tile != null)
            {
                curr_tile.curr_score *= 2;
            }
        }
    }

    //ID 9: Basil - Worth 0 pts. On plant, gains the current score of the plants in the tiles directly above, to the left, to the right, and below this plant
    void Basil(int row_num, int col_num)
    {
        LevelTile basil_tile = findTile(row_num, col_num);
        if(basil_tile == null)
        {
            Debug.Log("Basil plant action failed");
            return;
        }
        LevelTile curr_tile = null;

        //Tile directly above
        curr_tile = findTile(row_num, col_num - 1);
        if (curr_tile != null)
        {
            basil_tile.curr_score += curr_tile.curr_score;
        }

        //Tile direct to the left
        curr_tile = findTile(row_num - 1, col_num);
        if (curr_tile != null)
        {
            basil_tile.curr_score += curr_tile.curr_score;
        }

        //Tile direct to the right
        curr_tile = findTile(row_num + 1, col_num);
        if (curr_tile != null)
        {
            basil_tile.curr_score += curr_tile.curr_score;
        }

        //Tile directly below
        curr_tile = findTile(row_num, col_num + 1);
        if (curr_tile != null)
        {
            basil_tile.curr_score += curr_tile.curr_score;
        }
    }

    //ID 10: Sunburst Succulent - Worth 0 pts. Gives 2 points to each plant in the 8 adjacent tiles
    void SunSucc(int row_num,  int col_num)
    {
        LevelTile curr_tile = null;
        
        //top left
        curr_tile = findTile(row_num - 1, col_num - 1);
        if(curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //top mid
        curr_tile = findTile(row_num - 1, col_num);
        if (curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //top right
        curr_tile = findTile(row_num - 1, col_num + 1);
        if(curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //mid left
        curr_tile = findTile(row_num, col_num - 1);
        if (curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //mid right
        curr_tile = findTile(row_num, col_num + 1);
        if(curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //botttom left
        curr_tile = findTile(row_num + 1, col_num - 1);
        if(curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //bottom mid
        curr_tile = findTile(row_num + 1, col_num);
        if(curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }

        //bottom right
        curr_tile = findTile(row_num + 1, col_num + 1);
        if(curr_tile != null && curr_tile.getPlantType() > 0)
        {
            curr_tile.curr_score += 2;
        }
    }

    //ID 11: Vipergrass - Worth 1 pt. Steals half of the points from each plant in the same row
    //RECODE to a foreach loop that just checks the row parameter
    void Vipergrass(int row_num,  int col_num)
    {
        LevelTile viper_tile = findTile(row_num, col_num);
        if (viper_tile == null) {
            return;
        }
        viper_tile.curr_score += 1;
        LevelTile curr_tile = null;

        for(int i = 0; i < max_level_size; i++)
        {
            curr_tile = findTile(row_num, i);
            if(curr_tile != null && i != col_num)
            {
                int steal_this = curr_tile.curr_score / 2;
                curr_tile.curr_score -= steal_this;
                viper_tile.curr_score += steal_this;
            }
        }
    }

    //ID 12: Crabgrass - Worth 0 pts.
    //On plant: All plants in the same row lose 1 pt, though they cannot go below 0 pts.
    //This plant gains 2 pts for each plant in the same row
    void Crabgrass(int row_num, int col_num)
    {
        LevelTile crab_tile = findTile(row_num, col_num);
        if(crab_tile == null) {  return; }

        LevelTile curr_tile = null;

        foreach(GameObject tile in tiles)
        {
            curr_tile = tile.GetComponent<LevelTile>();
            if (curr_tile.row == row_num && curr_tile.col != col_num)
            {
                if (curr_tile.curr_score > 0) curr_tile.curr_score--;
                crab_tile.curr_score += 2;
            }
        }
    }

    //ID ??: Shiitake Mushrooms - Worth 0 pts. On plant, any plants worth 0 points
    //sitting in the tiles directly above, to the left, to the right, and below this plant are now worth 4 points
    void Shiitake(int row_num, int col_num)
    {
        //direct above
        LevelTile curr_tile = findTile(row_num - 1, col_num);
        if(curr_tile != null && curr_tile.curr_score == 0) { curr_tile.curr_score = 4; }

        //left
        curr_tile = findTile(row_num, col_num - 1);
        if (curr_tile != null && curr_tile.curr_score == 0) { curr_tile.curr_score = 4; }

        //right
        curr_tile = findTile(row_num, col_num + 1);
        if (curr_tile != null && curr_tile.curr_score == 0) { curr_tile.curr_score = 4; }

        //direct below
        curr_tile = findTile(row_num + 1, col_num);
        if (curr_tile != null && curr_tile.curr_score == 0) { curr_tile.curr_score = 4; }
    }

    //ID ??: Portabella Mushrooms - Worth 0 pts. On plant, if a plant in the same row is 0 pts, that plant is now worth 4 pts.
    //NOTICE: this plant seems liable to redesign
    void Portabella(int row_num, int col_num)
    {
        foreach(GameObject tile in tiles)
        {
            LevelTile curr_tile = tile.GetComponent<LevelTile>();
            if(curr_tile.row == row_num && curr_tile.col != col_num && curr_tile.curr_score == 0)
            {
                curr_tile.curr_score = 4;
            }
        }
    }


    //"Setup" plant actions : these actions wait until another action/condition is met
    //For setup plant actions the row_num and col_num parameters indicate the tile around which to check for setup plant actions,
    //not the location of the setup plant to activate

    //ID ??: Shameplant - Worth 5 pts on plant. AFter this plant has been planted, if another plant is plant in the
    //tiles directly above, to the left, to the right, or below it, it's point value is reduced to 0
    //This should be checked whenever a plant is planted
    //TODO : Update this method with the actual plantID of shameplant when thats done
    void checkShame(int row_num, int col_num)
    {
        //direct above
        LevelTile curr_tile = findTile(row_num - 1, col_num);
        if(curr_tile != null && curr_tile.getPlantType() == 999)
        {
            curr_tile.curr_score = 0;
        }

        //left
        curr_tile = findTile(row_num, col_num - 1);
        if (curr_tile != null && curr_tile.getPlantType() == 999)
        {
            curr_tile.curr_score = 0;
        }

        //right
        curr_tile = findTile(row_num, col_num + 1);
        if (curr_tile != null && curr_tile.getPlantType() == 999)
        {
            curr_tile.curr_score = 0;
        }

        //direct below
        curr_tile = findTile(row_num + 1, col_num);
        if (curr_tile != null && curr_tile.getPlantType() == 999)
        {
            curr_tile.curr_score = 0;
        }
    }


    //"Turn Counter" plant actions: This section checks all tiles for plantID's belonging to the counter based plants
    //It increments their counters and checks for their condition
    //If a plant has an on-plant action and a turn-counter based action, these are implemented separately in their respective sections
    
    //checks all plants for turn counter plants and calls their respective functions
    void incCounterPlants()
    {
        foreach(GameObject tile in tiles)
        {
            LevelTile tiledata = tile.GetComponent<LevelTile>();
            switch(tiledata.getPlantType()) {
                case 77: //Pear tree
                    PearTreeCounter(tiledata);
                    break;
                default:
                    break;
            }
        }
    }

    //ID ??: Pear Tree - Worth 0 pts on plant. After 5 other cards are planted, multiplies this plants points by 3
    void PearTreeCounter(LevelTile pear_data)
    {
        if(pear_data.incCounter() == 5)
        {
            pear_data.curr_score *= 3;
        }
    }
}