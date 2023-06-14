using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSwitch : MonoBehaviour
{
    [SerializeField] Transform acts;
    int currAct = 1;

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
            prevButton.SetActive(false);
        }else if(currAct == 3) {
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
        currAct = Mathf.Min(3, currAct + 1);
    }

    public void prevAct()
    {
        buttonAudio.Play();
        currAct = Mathf.Max(1, currAct - 1);
    }
}
