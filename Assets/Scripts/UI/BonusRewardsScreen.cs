using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusRewardsScreen : MonoBehaviour
{
    //array of the bonus levels
    GameObject[] bonusLevels;
    //Bonus points persistent data
    BonusPoints bonusPoints;

    // Start is called before the first frame update
    void Start()
    {
        bonusLevels = GameObject.FindGameObjectsWithTag("BonusIcon");
        bonusPoints = GameObject.Find("BonusPoints").GetComponent<BonusPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO - Figure out which level numbers are attached to what amount of bonus stars, then hide those icons which don't have enough stars for
        foreach(GameObject bonusLevel in bonusLevels)
        {
            BonusIcon currBonus = bonusLevel.GetComponent<BonusIcon>();
            if (currBonus != null && bonusPoints.GetStarCount() >= currBonus.GetStarsRequired())
            {
                bonusLevel.SetActive(true);
            }
            else
            {
                bonusLevel.SetActive(false);
            }
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
