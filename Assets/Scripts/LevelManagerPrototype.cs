using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerPrototype : MonoBehaviour
{
    const int max_level_size = 6;
    public int[,] level = new int[max_level_size, max_level_size];
    // Start is called before the first frame update
    void Start()
    {
        initializeArr();
        blockTile(1, 5);
        plantAction(3, 3, "test_plant_1");
        plantAction(2, 6, "test_plant_2");
        plantAction(1, 5, "test_plant_3");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(numTiles());   
    }

    //initializes the level array with all valid tiles
    void initializeArr()
    {
        for (int i = 0; i < max_level_size; i++)
        {
            for (int j = 0; j < max_level_size; j++)
            {
                level[i, j] = 1;
            }
        }
    }

    //blocks a tile, such that it is no longer a part of the level
    public void blockTile(int x, int y)
    {
        if(x < 0 || x >= max_level_size || y < 0 || y >= max_level_size)
        {
            Debug.Log("Invalid blockTile args");
        }
        else
        {
            level[x, y] = -1;
        }
    }

    //DEBUGGING
    //counts number of tiles being used in the level
    int numTiles()
    {
        int tileCount = 0;
        for (int i = 0; i < max_level_size; i++)
        {
            for (int j = 0; j < max_level_size; j++)
            {
                if (level[i, j] >= 0)
                {
                    tileCount++;
                }
            }
        }
        return tileCount;
    }

    //checks if a tile position is valid for a plant
    //consider making this more robust to return a specific value corresponding to a specific plant
    bool checkValidTile(int x, int y)
    {
        //checks if tile position is outside of the level
        if(x < 0 || x >= max_level_size || y < 0 || y >= max_level_size)
        {
            return false;
        }

        //checks if tile position is blocked/not a part of the level
        if (level[x, y] < 0)
        {
            return false;
        }
        return true;
    }

    //DEBUGGING
    //Given x and y positions of a plant (base_x and base_y respectively), performs actions on the eight tiles adjacent to the block
    //If the plant is on a border, only perform actions on valid tiles
    //The string plant is just a placeholder, in the future this should be the full plant object
    void plantAction(int base_x, int base_y, string plant)
    {
        if(!checkValidTile(base_x, base_y))
        {
            Debug.Log("Invalid plantAction location");
            return;
        }
        //int curr_x, curr_y; //These are the x and y positions of the tile we are checking
        //Check tiles in this order
        //  0   1   2
        //  3   _   4
        //  5   6   7

        //check 0
        if(checkValidTile(base_x - 1, base_y - 1))
        {
            plantActionHelper(base_x - 1, base_y - 1, plant);
        }

    }

    //x and y is the position of the tile to perform the action on
    //parentPlant is the plant performing its action on tile (x, y)
    void plantActionHelper(int x, int y, string parentPlant)
    {
        //call the parentPlants action method
        Debug.Log(parentPlant + " is doing action on tile (" + x + ", " + y + ")");
    }
}
