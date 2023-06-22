using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    /*
     * music player states
     * 0 - title
     * 1 - act 1 cutscene
     * 2 - act 1 levels
     * 3 - act 2 cutscene
     * 4 - act 2 levels
     * 5 - act 3 cutscene
     * 6 - act 3 levels
     * */
    [SerializeField] int musicState;

    //returns the music player state for this screen
    public int GetMusicState()
    {
        return musicState;
    }
}
