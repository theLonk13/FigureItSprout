using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is redundant at this point, look to cut out an djust use LevelManager.ResetLevel()
public class ResetLevel : MonoBehaviour
{
    [SerializeField] LevelManager lvMan;
    public void resetLevel()
    {
        lvMan.ResetLevel();
    }
}
