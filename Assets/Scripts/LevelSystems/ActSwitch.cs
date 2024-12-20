using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActSwitch : MonoBehaviour
{
    [SerializeField] Transform acts;
    int currAct = 1;
    [SerializeField] int numActs = 3;

    [SerializeField] GameObject prevButton;
    [SerializeField] GameObject nextButton;

    //audio for button
    AudioSource buttonAudio;

    void Start()
    {
        //currAct = 1;
        buttonAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        prevButton.SetActive(true);
        nextButton.SetActive(true);

        placeActs();
        if(currAct == 1)
        {
            //prevButton.SetActive(false);
        }else if(currAct >= numActs) {
            nextButton.SetActive(false);
        }
    }

    void placeActs()
    {
        acts.localPosition = new Vector3(-1000*(currAct-1), 0, 0);
    }

    public void changeAct(int actNum)
    {
        currAct = actNum;
    }

    public void nextAct()
    {
        buttonAudio.Play();
        currAct = Mathf.Min(numActs, currAct + 1);
    }

    public void prevAct()
    {
        buttonAudio.Play();
        if(currAct == 1) { SceneManager.LoadScene("TitleScreen"); }
        currAct = Mathf.Max(1, currAct - 1);
    }
}
