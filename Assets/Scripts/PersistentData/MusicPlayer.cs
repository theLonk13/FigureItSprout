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

    [SerializeField] float act1ResetVol = 1.0f;
    [SerializeField] float act2ResetVol = .6f;
    [SerializeField] float act3ResetVol = .8f;

    /*
     * music player states
     * 0 - title
     * 1 - act 1 cutscene
     * 2 - act 1 levels
     * 3 - act 2 cutscene
     * 4 - act 2 levels
     * 5 - act 3 cutscene
     * 6 - act 3 levels
     * 7 - cutscene, no music
     * */
    int playerState = 0;

    //current player
    AudioSource currPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        if(titleMusic != null) titleMusic.Play();
        Debug.LogError("Force Open dev console");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        SceneData currScene = null;
        GameObject sceneDataObj = GameObject.Find("SceneData");
        if (sceneDataObj != null) { currScene = sceneDataObj.GetComponent<SceneData>(); }
        if(currScene != null) { 
            if(playerState != currScene.GetMusicState())
            {
                Debug.Log("New Player State detected : " + currScene.GetMusicState() + "\tOld state : " + playerState);
                StartCoroutine(MusicFade(4.0f, 0f));
            }
            playerState = currScene.GetMusicState();
        }

        //StopAllSound(playerState);

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
            Debug.Log("Playing music state " + playerState);
            currPlayer.Play();
        }
        */
    }

    void StopAllSound(int playerState)
    {
        if(playerState != 0 && titleMusic.isPlaying) {
            StartCoroutine(MusicFade(titleMusic, 2.0f, 0f, 1f));
        }
        if (playerState != 1 && act1Cutscene.isPlaying) { StartCoroutine(MusicFade(act1Cutscene, 2.0f, 0f, 1f)); }
        if (playerState != 2 && act1Levels.isPlaying) { StartCoroutine(MusicFade(act1Levels, 2.0f, 0f, act1ResetVol)); }
        if (playerState != 3 && act2Cutscene.isPlaying) { StartCoroutine(MusicFade(act2Cutscene, 2.0f, 0f, 1f)); }
        if (playerState != 4 && act2Levels.isPlaying) { StartCoroutine(MusicFade(act2Levels, 2.0f, 0f, act2ResetVol)); }
        if (playerState != 5 && act3Cutscene.isPlaying) { StartCoroutine(MusicFade(act3Cutscene, 2.0f, 0f, 1f)); }
        if (playerState != 6 && act3Levels.isPlaying) { StartCoroutine(MusicFade(act3Levels, 2.0f, 0f, act3ResetVol)); }
    }

    public void PlayTitle()
    {
        StopAllSound(0);
        if (!titleMusic.isPlaying)
        {
            titleMusic.Play();
        }
        playerState = 0;
    }

    public void PlayAct1Cutscene()
    {
        StopAllSound(1);
        if(!act1Cutscene.isPlaying)
        {
            act1Cutscene.Play();
        }
        playerState = 1;
    }

    public void PlayAct2Cutscene()
    {
        StopAllSound(3);
        if (!act2Cutscene.isPlaying)
        {
            act2Cutscene.Play();
        }
        playerState = 3;
    }

    public void PlayAct3Cutscene()
    {
        StopAllSound(5);
        if (!act3Cutscene.isPlaying)
        {
            act3Cutscene.Play();
        }
        playerState = 5;
    }

    public void PlayAct1Levels()
    {
        StopAllSound(2);
        if (!act1Levels.isPlaying)
        {
            act1Levels.Play();
        }
        playerState = 2;
    }

    public void PlayAct2Levels()
    {
        StopAllSound(4);
        if (!act2Levels.isPlaying)
        {
            act2Levels.Play();
        }
        playerState = 4;
    }

    public void PlayAct3Levels()
    {
        StopAllSound(6);
        if (!act3Levels.isPlaying)
        {
            act3Levels.Play();
        }
        playerState = 6;
    }

    IEnumerator MusicFade(AudioSource audioSource, float duration, float targetVolume, float resetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.Stop();
        //SceneData sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        //sceneData.ChangeMusicState(7);
        audioSource.volume = resetVolume;
        yield break;
    }
}
