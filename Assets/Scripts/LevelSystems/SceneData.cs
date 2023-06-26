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

    void Awake()
    {
        MusicPlayer musicPlayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        if(musicPlayer != null)
        {
            switch(musicState)
            {
                case 0:
                    musicPlayer.PlayTitle();
                    break;
                case 1:
                    musicPlayer.PlayAct1Cutscene();
                    break;
                case 2:
                    musicPlayer.PlayAct1Levels();
                    break;
                case 3:
                    musicPlayer.PlayAct2Cutscene();
                    break;
                case 4:
                    musicPlayer.PlayAct2Levels();
                    break;
                case 5:
                    musicPlayer.PlayAct3Cutscene();
                    break;
                case 6:
                    musicPlayer.PlayAct3Levels();
                    break;
            }
        }
    }

    //returns the music player state for this screen
    public int GetMusicState()
    {
        return musicState;
    }

    public void ChangeMusicState(int newState)
    {
        musicState = newState;
    }
}
