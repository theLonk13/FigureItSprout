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
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(titleMusic != null) titleMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StopAllSound()
    {
        titleMusic.Stop();
        act1Cutscene.Stop();
        act1Levels.Stop();
        act2Cutscene.Stop();
        act2Levels.Stop();
        act3Cutscene.Stop();
        act3Levels.Stop();
    }

    public void PlayTitle()
    {
        StopAllSound();
        titleMusic.Play();
    }

    public void PlayAct1Cutscene()
    {
        StopAllSound();
        act1Cutscene.Play();
    }

    public void PlayAct2Cutscene()
    {
        StopAllSound();
        act2Cutscene.Play();
    }

    public void PlayAct3Cutscene()
    {
        StopAllSound();
        act3Cutscene.Play();
    }

    public void PlayAct1Levels()
    {
        StopAllSound();
        act1Levels.Play();
    }

    public void PlayAct2Levels()
    {
        StopAllSound();
        act2Levels.Play();
    }

    public void PlayAct3Levvels()
    {
        StopAllSound();
        act3Levels.Play();
    }
}
