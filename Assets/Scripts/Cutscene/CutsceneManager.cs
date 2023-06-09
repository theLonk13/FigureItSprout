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

    // Start is called before the first frame update
    void Start()
    {
        frames = GameObject.FindGameObjectsWithTag("CutsceneFrame");
        FindFrame(currFrameNum);
        AdvanceFrame();

    }

    // Update is called once per frame
    void Update()
    {
        //if(currFrameNum == 0) { AdvanceFrame(); }
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
        //if current frame is not done fading in, either start or quick finish fading in
        if(currFrame.GetFrameState() != 2)
        {
            currFrame.AdvanceFrame();
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
                Invoke("LoadLevel", 1);
            }
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(lvName);
    }
}
