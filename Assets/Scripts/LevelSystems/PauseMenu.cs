using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //The gameobject that is the parent of the pause menu buttons
    [SerializeField] GameObject menu;

    //turns on/off the pause menu
    public void toggleMenu()
    {
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
}
