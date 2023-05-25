using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTracker : MonoBehaviour
{
    // notes the level the current hint counter is for
    int lvNum = -1;
    // tracks how many hints the player has accepted
    int hintsAccepted = 0;
    // tracks number of fails since the last hint accept
    int localFails = 0;

    // tracks if the current attempt of the level has been failed
    bool failed = false;

    //checks if the current level has been failed
    void Update()
    {
        if (!failed)
        {
            //checks for no cards remaining
            GameObject[] cardsLeft = GameObject.FindGameObjectsWithTag("PlantCard");
            if(cardsLeft.Length <= 0)
            {
                failed = true;
                localFails++;
                return;
            }

            //checks for no tiles remaining
            //TODO : 
        }

        debug_DisplayHintData();
    }

    //checks if the current hint counter is for this level
    //if not, update the level number and reset all counters
    //should be performed on all level loads
    public void checkLevel(int lvNum)
    {
        failed = false;
        if(lvNum == this.lvNum) { return; }

        this.lvNum = lvNum;
        hintsAccepted = 0;
        localFails = 0;
    }

    //returns the number of level fails since the last hint was taken
    public int getLocalFails()
    {
        return localFails;
    }

    //returns the number of hints the player has accepted this level
    public int getHintsAccepted()
    {
        return hintsAccepted;
    }

    public void AcceptHint()
    {
        hintsAccepted++;
        localFails = 0;
    }

    void debug_DisplayHintData()
    {
        Debug.Log("Hint Data: Level " + lvNum + "\tHints Accepted: " + hintsAccepted + "\tLocal Fails: " + localFails);
    }
}
