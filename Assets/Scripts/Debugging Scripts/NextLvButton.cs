using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvButton : MonoBehaviour
{
    GameObject nextLvButton;
    LevelManager lvManager;

    // Start is called before the first frame update
    void Start()
    {
        nextLvButton = GameObject.FindWithTag("debug_NextLv");
        lvManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lvManager.GoalMet())
        {
            nextLvButton.SetActive(true);
        }
        else
        {
            nextLvButton.SetActive(false);
        }
    }

    public void nextScene()
    {
        MomPlantData mom_plant = GameObject.Find("MomPlantData").GetComponent<MomPlantData>();
        if(mom_plant != null)
        {
            mom_plant.saveOrchid();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
