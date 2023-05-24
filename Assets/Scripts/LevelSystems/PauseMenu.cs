using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //The gameobject that is the parent of the pause menu buttons
    [SerializeField] GameObject menu;

    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && menu.activeSelf) { toggleMenu(); }
    }
    */

    //turns on/off the pause menu
    public void toggleMenu()
    {
        Debug.Log("Toggling Pause Menu");
        if(menu == null) { return; }

        if(menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
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

    public void quit()
    {
        Application.Quit();
    }
}
