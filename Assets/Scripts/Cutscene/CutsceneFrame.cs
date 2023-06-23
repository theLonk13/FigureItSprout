using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneFrame : MonoBehaviour
{
    [SerializeField] int frameNum = 0;

    //interactible object in cutscene frame;
    [SerializeField] GameObject Interactible;
    //object triggered by Interactible
    [SerializeField] GameObject InteractibleTriggered;
    RectTransform InteractibleRect;
    bool interactibleTriggered = false;

    Vector3 showPos = new Vector3(0, 0, 0);
    Vector3 hidePos = new Vector3(0, -1000, 0);

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

        if(InteractibleTriggered != null)
        {
            InteractibleRect = InteractibleTriggered.GetComponent<RectTransform>();
        }
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
            Image interactibleImage = null;
            if (Interactible != null)
            {
                Interactible.SetActive(true);
                interactibleImage = Interactible.GetComponent<Image>();
            }

            //fading in
            Color tempColor = frameImage.color;
            tempColor.a = Mathf.Min(tempColor.a + .02f, 1.0f);
            frameImage.color = tempColor;
            if(interactibleImage != null){ interactibleImage.color = tempColor; }

            //update state when finished fading in
            if(tempColor.a >= 1.0f)
            {
                frameState = 2;
                
            }
        }
        else
        {

        }

        if (interactibleTriggered && InteractibleTriggered != false)
        {
            InteractibleRect.localPosition = Vector3.MoveTowards(InteractibleRect.localPosition, showPos, Time.deltaTime * 4000);
        }
        else if (InteractibleRect != null)
        {
            InteractibleRect.localPosition = Vector3.MoveTowards(InteractibleRect.localPosition, hidePos, Time.deltaTime * 4000);
        }
    }

    public void AdvanceFrame()
    {
        //If frame is not active, start fading in
        if(frameState == 0)
        {
            frameState = 1;
            //moving this frame to end of child list
            //*
            int numChildren = this.transform.parent.gameObject.transform.childCount;
            this.transform.SetSiblingIndex(numChildren - 1);
            //turn raycast off so nextFrameButton can still be clicked
            if(Interactible == null)
            {
                frameImage.raycastTarget = false;
            }
            //*/
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
            //If there is an interactible, do nothing
            //if (Interactible != null) { return; }

            frameState = 0;
        }
    }

    public void ToggleInteractible()
    {
        if(interactibleTriggered == true) { interactibleTriggered = false; }
        else if(InteractibleTriggered != null && frameState == 2) {
            interactibleTriggered = true;
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

    public bool GetHasUntriggeredInteractible()
    {
        return Interactible != null && !interactibleTriggered;
    }
}
