using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    int currFrameNum = 0;
    GameObject[] frames;

    CutsceneFrame currFrame;
    //bool frameReady = true;

    //Level to be played after this scene
    [SerializeField] string lvName;
    //GameObject that is the mouse click indicator
    [SerializeField] GameObject mouseClickObj;
    //toggle for showing mouse click
    bool showMouseClick = false;
    //timer for showing mouse click
    float timer = 1.0f;
    //positions for mouse click
    Vector3 showPos = new Vector3(0, 0, 0);
    Vector3 hidePos = new Vector3(0, -200, 0);

    //variables for autoplaying cutscene
    [SerializeField] bool autoPlay = false;
    [SerializeField] float autoPlayDelay = 2.0f;
    bool nextAutoPlayReady = false;

    //audio for button
    [SerializeField] AudioSource buttonAudio;

    //delay for moving to level after scene
    [SerializeField] float nextLvDelay = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        frames = GameObject.FindGameObjectsWithTag("CutsceneFrame");
        FindFrame(currFrameNum);
        if (autoPlay) { 
            //AdvanceFrame();
            //Invoke("AdvanceFrame", autoPlayDelay);
        }
        else
        {
            AdvanceFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //buttonAudio = GetComponent<AudioSource>();
        //if(currFrameNum == 0) { AdvanceFrame(); }

        //increment timer
        timer += Time.deltaTime;
        if(timer > 4.0f)
        {
            showMouseClick = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //mouseClickObj.SetActive(false);
            timer = 0.0f;
            showMouseClick = false;
        }

        if(showMouseClick && !autoPlay)
        {
            mouseClickObj.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(mouseClickObj.GetComponent<RectTransform>().localPosition, showPos, Time.deltaTime * 1000);
        }
        else
        {
            mouseClickObj.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(mouseClickObj.GetComponent<RectTransform>().localPosition, hidePos, Time.deltaTime * 1000);
        }
        
        //*
        if(autoPlay && nextAutoPlayReady)
        {
            nextAutoPlayReady = false;
            Invoke("AdvanceFrame", autoPlayDelay);
        }
        //*/
    }

    //Loads a frame into the currFrame variable based on the given frame number parameter
    void FindFrame(int frameNum)
    {
        foreach(GameObject frame in frames)
        {
            CutsceneFrame tempFrame = frame.GetComponent<CutsceneFrame>();
            if(tempFrame != null && tempFrame.GetFrameNum() == currFrameNum)
            {
                currFrame = tempFrame;
                return;
            }
        }

        currFrame = null;
    }

    public void AdvanceFrame()
    {
        if (autoPlay)
        {
            Debug.Log("Advance Frame autoplaying");
            if(currFrame.GetFrameState() != 2)
            {
                Invoke("AdvanceFrame", .1f);
                if(currFrame.GetFrameState() == 0)
                {
                    currFrame.AdvanceFrame();
                }
            }
            else
            {
                Invoke("AutoAdvanceHelper", autoPlayDelay);
            }
            return;
        }
        buttonAudio.Play();
        //if current frame is not done fading in, either start or quick finish fading in
        if(currFrame.GetFrameState() != 2)
        {
            currFrame.AdvanceFrame();
            //if(autoPlay) { Invoke("AdvanceFrame", autoPlayDelay); }
        }
        else if (currFrame.GetHasUntriggeredInteractible())
        {
            //if current frame has an interactible, do nothing
            return;
        }
        else
        {
            //start fading out current frame
            currFrame.AdvanceFrame();
            //if current frame was fully faded in, move to next scene and start fading
            FindFrame(++currFrameNum);
            if(currFrame != null)
            {
                AdvanceFrame();
            }
            else
            {
                Invoke("LoadLevel", nextLvDelay);
            }
        }
    }

    public void AutoAdvanceHelper()
    {
        currFrame.AdvanceFrame();
        FindFrame(++currFrameNum);
        if (currFrame != null)
        {
            Debug.Log("Moving to next frame");
            //nextAutoPlayReady = true;
            Invoke("AdvanceFrame", 1f);
            //AdvanceFrame();
        }
        else
        {
            GameObject phone = GameObject.Find("Phone");
            if (phone != null)
            {
                phone.GetComponent<Lv21Phone>().DelayedDecline();
            }
            Invoke("LoadLevel", autoPlayDelay);
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(lvName);
    }
}
