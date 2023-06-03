using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusRewardsScreen : MonoBehaviour
{
    //array of the bonus levels
    GameObject[] bonusLevels;

    // Start is called before the first frame update
    void Start()
    {
        bonusLevels = GameObject.FindGameObjectsWithTag("LevelSelector");
    }

    // Update is called once per frame
    void Update()
    {
        //TODO - Figure out which level numbers are attached to what amount of bonus stars, then hide those icons which don't have enough stars for
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
