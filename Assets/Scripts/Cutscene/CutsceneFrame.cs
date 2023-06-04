using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneFrame : MonoBehaviour
{
    [SerializeField] int frameNum = 0;

    Image frameImage;
    //bool activeFrame = false;
    int frameState = 0;
    //State chart:
    /*
     * 0: Inactive
     * 1: Fading in
     * 2: Fully visible
     */

    // Start is called before the first frame update
    void Start()
    {
        frameImage = GetComponent<Image>();

        //set image to invisible
        Color tempColor = frameImage.color;
        tempColor.a = 0f;
        frameImage.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(frameState == 0)
        {
            //fading out
            Color tempColor = frameImage.color;
            tempColor.a = Mathf.Max(tempColor.a - .02f, 0f);
            frameImage.color = tempColor;
        }
        else if(frameState == 1)
        {
            //fading in
            Color tempColor = frameImage.color;
            tempColor.a = Mathf.Min(tempColor.a + .02f, 1.0f);
            frameImage.color = tempColor;

            //update state when finished fading in
            if(tempColor.a >= 1.0f)
            {
                frameState = 2;
            }
        }
        else
        {

        }
    }

    public void AdvanceFrame()
    {
        //If frame is not active, start fading in
        if(frameState == 0)
        {
            frameState = 1;
        }
        else if(frameState == 1)
        {
            //If frame is currently fading in, quick finish to full image
            Color tempColor = frameImage.color;
            tempColor.a = 1.0f;
            frameImage.color = tempColor;
            //Set state
            frameState = 2;
        }else if(frameState == 2)
        {
            frameState = 0;
        }
    }

    public int GetFrameNum()
    {
        return frameNum;
    }

    public int GetFrameState()
    {
        return frameState;
    }
}
