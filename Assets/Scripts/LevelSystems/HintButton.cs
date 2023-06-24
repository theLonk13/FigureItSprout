using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintButton : MonoBehaviour
{
    HintTracker hintTracker;
    [SerializeField] GameObject hintButton;
    [SerializeField] LevelManager lvMan;
    //general hint info
    [SerializeField] int numHints; //total number of available hints for this level
    [SerializeField] int failTolerance; //number of level failures before the hint button is shown

    //audio for button click
    AudioSource buttonAudio;

    //hint info
    //These should be in order, i.e. hint 1 should be the first plant down and hint 2 should be the second plant
    // hint 1 info
    [SerializeField] GameObject plant1;
    [SerializeField] int row1;
    [SerializeField] int col1;

    //hint 2 info
    [SerializeField] GameObject plant2;
    [SerializeField] int row2;
    [SerializeField] int col2;

    // Start is called before the first frame update
    void Awake()
    {
        hintTracker = GameObject.Find("HintTracker").GetComponent<HintTracker>();
        buttonAudio = GetComponent<AudioSource>();
        Invoke("ShowHints", .2f);
        //ShowHintButton();
    }

    // Update is called once per frame
    void Update()
    {
        ShowHintButton();
        //ShowHints();
    }

    public void ShowHintButton()
    {
        if((hintTracker.getHintsAccepted() >= numHints) || (hintTracker.getLocalFails() <= failTolerance))
        {
            hintButton.SetActive(false);
        }
        else
        {
            hintButton.SetActive(true);
        }
    }

    public void AcceptHint()
    {
        buttonAudio.Play();
        hintTracker.AcceptHint();
        Destroy(hintButton);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ShowHints()
    {
        ShowHint1();
        Invoke("ShowHint2", .5f);
        //ShowHint2();
    }

    void ShowHint1()
    {
        Debug.LogError("Showing Hint 1");
        if (hintTracker.getHintsAccepted() >= 1 && plant1 != null)
        {
            LevelTile tile = lvMan.findTile(row1, col1);
            tile.plantPlant(plant1.GetComponent<PlantCard>().getPlantID());
            Destroy(plant1);
        }
    }

    void ShowHint2()
    {
        Debug.LogError("Showing Hint 2");
        if (hintTracker.getHintsAccepted() >= 2 && plant2 != null)
        {
            LevelTile tile = lvMan.findTile(row2, col2);
            tile.plantPlant(plant2.GetComponent<PlantCard>().getPlantID());
            Destroy(plant2);
        }
    }
}
