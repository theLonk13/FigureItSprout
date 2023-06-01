using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedPlants : MonoBehaviour
{
    //toggles playtesting mode
    [SerializeField] bool playTestMode;

    // number of pages in our book
    [SerializeField] int numPages;

    //holds the ids of all plants the player has unlocked
    int[] plant_unlocks;
    int numUnlocks = 0;

    void Start()
    {
        plant_unlocks = new int[numPages];
    }

    void Update()
    {
        //Debug.Log(plant_unlocks);
        debug_ShowAllUnlocks();
    }

    //adds a plant to the unlocked list if not already on it
    public void UnlockPlant(int plantID)
    {
        if(plantID > 0 && numUnlocks < numPages)
        {
            foreach(int i in plant_unlocks)
            {
                if (i == plantID) return;
            }
            plant_unlocks[numUnlocks++] = plantID;
        }
    }

    //returns a list of all plants the player has seen
    public int[] get_plant_unlocks(int lvNum)
    {
        if(!playTestMode) return plant_unlocks;

        int[] playtest_unlocks = new int[numPages];
        int numUnlocks = 0;
        if(lvNum > 0) { playtest_unlocks[numUnlocks++] = 1; }
        if (lvNum >= 2) { playtest_unlocks[numUnlocks++] = 2; playtest_unlocks[numUnlocks++] = 5; }
        if (lvNum >= 4) { playtest_unlocks[numUnlocks++] = 9; }
        if (lvNum >= 5) { playtest_unlocks[numUnlocks++] = 3; }
        if (lvNum >= 6) { playtest_unlocks[numUnlocks++] = 7; }
        if (lvNum >= 8) { playtest_unlocks[numUnlocks++] = 10; }
        if (lvNum >= 11) { playtest_unlocks[numUnlocks++] = 11; }
        if (lvNum >= 13) { playtest_unlocks[numUnlocks++] = 12; }
        if (lvNum >= 14) { playtest_unlocks[numUnlocks++] = 8; }
        if (lvNum >= 15) { playtest_unlocks[numUnlocks++] = 13; }
        if (lvNum >= 17) { playtest_unlocks[numUnlocks++] = 15; playtest_unlocks[numUnlocks++] = 16; }
        if(lvNum >= 22) { plant_unlocks[numUnlocks++] = 6; }
        if (lvNum >= 26) { plant_unlocks[numUnlocks++] = 17; }
        if(lvNum >= 28) { plant_unlocks[numUnlocks++]= 18; }
        return playtest_unlocks;
    }

    void debug_ShowAllUnlocks()
    {
        string unlocks = "";
        foreach(int unlock in plant_unlocks)
        {
            unlocks += unlock + " ";
        }
        Debug.Log("Plant Unlocks:\n" + unlocks);
    }
}
