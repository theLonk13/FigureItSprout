using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //The gameobject that is the parent of the pause menu buttons
    [SerializeField] GameObject menu;

    //GameObjects for the reset hint button
    [SerializeField] GameObject resetHint;
    HintTracker hintTracker;

    //audiosource for button click
    [SerializeField] AudioSource buttonAudio;

    void Start()
    {
        hintTracker = GameObject.Find("HintTracker").GetComponent<HintTracker>();
        //buttonAudio = GetComponent<AudioSource>();
        if(menu != null)
        {
            menu.SetActive(false);
        }
    }

    //*
    void Update()
    {
        if(hintTracker != null)
        {
            Debug.Log("Searching for  hinttracker");
            hintTracker = GameObject.Find("HintTracker").GetComponent<HintTracker>();
        }
    }
    //*/

    //turns on/off the pause menu
    public void toggleMenu()
    {
        //Debug.Log("Toggling Pause Menu");
        if(menu == null) { return; }

        if(menu.activeSelf)
        {
            buttonAudio.Play();
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            buttonAudio.Play();
        }

        //Debug.Log(hintTracker.getHintsAccepted());
        if (hintTracker != null && hintTracker.getHintsAccepted() > 0) 
        { 
            resetHint.SetActive(true); 
        }
        else { resetHint.SetActive(false); }
    }

    //reset the level
    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //go to the level select screen
    public void goToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    //reset hint counter
    public void ResetHints()
    {
        hintTracker.ResetHints();
    }

    public void quit()
    {
        Application.Quit();
    }
}
