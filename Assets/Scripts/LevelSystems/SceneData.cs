using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [SerializeField] int musicState;

    //returns the music player state for this screen
    public int GetMusicState()
    {
        return musicState;
    }
}
