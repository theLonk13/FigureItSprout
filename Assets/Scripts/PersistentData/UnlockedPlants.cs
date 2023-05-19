using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedPlants : MonoBehaviour
{
    [SerializeField] int numPages;

    int[] plant_unlocks;
    int numUnlocks = 0;

    // Start is called before the first frame update
    void Start()
    {
        plant_unlocks = new int[numPages];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(plant_unlocks);
        debug_ShowAllUnlocks();
    }

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
    public int[] get_plant_unlocks()
    {
        return plant_unlocks;
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
