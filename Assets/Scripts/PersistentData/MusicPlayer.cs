using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //AudioSources for all background music/sounds
    [SerializeField] AudioSource titleMusic;
    [SerializeField] AudioSource act1Cutscene;
    [SerializeField] AudioSource act1Levels;
    [SerializeField] AudioSource act2Cutscene;
    [SerializeField] AudioSource act2Levels;
    [SerializeField] AudioSource act3Cutscene;
    [SerializeField] AudioSource act3Levels;

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
    int playerState = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if(titleMusic != null) titleMusic.Play();
        Debug.LogError("Force Open dev console");
    }

    // Update is called once per frame
    void Update()
    {
        SceneData currScene = null;
        GameObject sceneDataObj = GameObject.Find("SceneData");
        if (sceneDataObj != null) { currScene = sceneDataObj.GetComponent<SceneData>(); }
        if(currScene != null) { playerState = currScene.GetMusicState(); }
        StopAllSound(playerState);

        AudioSource currPlayer = null;
        //play the appropriate music for the current screen
        switch(playerState)
        {
            case 0:
                currPlayer = titleMusic; break;
            case 1:
                currPlayer = act1Cutscene; break;
            case 2:
                currPlayer = act1Levels; break;
            case 3:
                currPlayer = act2Cutscene; break;
            case 4:
                currPlayer = act2Levels; break;
            case 5:
                currPlayer = act3Cutscene; break;
            case 6:
                currPlayer = act3Levels; break;
        }

        if(currPlayer != null && currPlayer.clip != null && !currPlayer.isPlaying)
        {
            currPlayer.Play();
        }
    }

    void StopAllSound(int playerState)
    {
        if(playerState != 0) { titleMusic.Stop(); }
        if (playerState != 1) { act1Cutscene.Stop(); }
        if (playerState != 2) { act1Levels.Stop(); }
        if (playerState != 3) { act2Cutscene.Stop(); }
        if (playerState != 4) { act2Levels.Stop(); }
        if (playerState != 5) { act3Cutscene.Stop(); }
        if (playerState != 6) { act3Levels.Stop(); }
    }

    public void PlayTitle()
    {
        StopAllSound(0);
        titleMusic.Play();
        playerState = 0;
    }

    public void PlayAct1Cutscene()
    {
        StopAllSound(1);
        act1Cutscene.Play();
        playerState = 1;
    }

    public void PlayAct2Cutscene()
    {
        StopAllSound(3);
        act2Cutscene.Play();
        playerState = 3;
    }

    public void PlayAct3Cutscene()
    {
        StopAllSound(5);
        act3Cutscene.Play();
        playerState = 5;
    }

    public void PlayAct1Levels()
    {
        StopAllSound(2);
        act1Levels.Play();
        playerState = 2;
    }

    public void PlayAct2Levels()
    {
        StopAllSound(4);
        act2Levels.Play();
        playerState = 4;
    }

    public void PlayAct3Levvels()
    {
        StopAllSound(6);
        act3Levels.Play();
        playerState = 6;
    }
}
