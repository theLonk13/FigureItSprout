using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneCutsceneManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueMan;
    [SerializeField] DialogueTrigger dialogueStart;

    [SerializeField] GameObject messageContainer;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject messagePhotoPrefab;

    [SerializeField] string nextScene;
    [SerializeField] Animator fadeAnim;
    [SerializeField] Lv21Cutscene lv21cutscene;

    [SerializeField] Animator currMsgAnimator;

    [SerializeField] DialogueTrigger[] dialogueTriggers;
    int nextDialogueTrigger = 0;
    [SerializeField] Sprite[] photoMsgs;
    int nextPhotoMsg = 0;
    bool photoNext = false;
    Vector2 msgContainerBottom;

    [SerializeField] GameObject[] messageQueue;
    int nextMessage = 0;

    [SerializeField] int[] msgSenderIDs;
    [SerializeField] int[] msgSizes;
    int currMsg = 0;
    int lastMsgSize = 0;

    bool msgShown = false;

    //mouse click/drag handling stuff
    bool mouseDown = false;
    float mouseDownTimer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("DelayedStart", .2f);
    }

    void DelayedStart()
    {
        fadeAnim.SetBool("ShowPhone", true);
        //dialogueStart.TriggerDialogue();
        GetNextMessage();
        //AdvanceDialogue();
        if (currMsgAnimator != null)
        {
            currMsgAnimator.SetBool("ShowMsg", true);
        }
        msgShown = true;

        RectTransform msgContainerRect = messageContainer.GetComponent<RectTransform>();
        msgContainerBottom = msgContainerRect.offsetMin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTimer = 0f;
            mouseDown = true;
        }else if (Input.GetMouseButtonUp(0))
        {
            if(mouseDownTimer < .2f)
            {
                AdvanceDialogue();
            }
            mouseDown = false;
        }

        if(mouseDown)
        {
            mouseDownTimer += Time.deltaTime;
            Debug.Log(mouseDownTimer);
        }
    }

    public void AdvanceDialogue()
    {
        if(dialogueMan != null && dialogueMan.CheckCurrDialogue())
        {
            Debug.Log("Check 1");
            dialogueMan.DisplayNextSentence();
        }
        else if(!msgShown){
            Debug.Log("Check 2");
            if(currMsgAnimator != null)
            {
                currMsgAnimator.SetBool("ShowMsg", true);
            }
            dialogueMan.DisplayNextSentence();
            msgShown = true;
        }
        else if(dialogueMan.CheckSentencesLeft() != 0)
        {
            Debug.Log("Check 3");
            Debug.Log("Continuing current dialogue");
            GameObject newMessage = Instantiate(messagePrefab, messageContainer.transform);
            PhoneMessageScript msgScript = newMessage.GetComponent<PhoneMessageScript>();

            if(currMsg < msgSenderIDs.Length)
            {
                msgScript.SetMsgProps(msgSenderIDs[currMsg], msgSizes[currMsg]);
                lastMsgSize = msgSizes[currMsg];
                currMsg++;
            }

            dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            currMsgAnimator = msgScript.GetMsgAnimator();

            currMsgAnimator.SetBool("ShowMsg", true);
            dialogueMan.DisplayNextSentence();

            msgShown = true;

            RectTransform msgContainerRect = messageContainer.GetComponent<RectTransform>();
            if (msgContainerRect != null && currMsg > 1)
            {
                IncreaseMsgContainerSize(lastMsgSize);
                /*
                switch (lastMsgSize)
                {
                    case 0:

                }
                msgContainerRect.offsetMax = new Vector2(msgContainerRect.offsetMax.x, msgContainerRect.offsetMax.y + 20);
                msgContainerRect.offsetMin = new Vector2(msgContainerBottom.x, msgContainerBottom.y);
                */
            }
        }
        else
        {
            Debug.Log("Check 4");
            GetNextMessage() ;
        }
    }

    void GetNextMessage()
    {
        if(nextMessage >= messageQueue.Length) {
            Debug.Log("No more messages");
            fadeAnim.SetBool("ShowPhone", false);
            if(lv21cutscene != null)
            {
                Invoke("Lv21Helper", 3f);
            }
            else
            {
                Invoke("LoadNextScene", 3f);
            }
            return;
        }

        GameObject nextMsg = messageQueue[nextMessage];
        nextMessage++;

        if(nextMsg.GetComponent<DialogueTrigger>() != null)
        {
            Debug.Log("Creating new text message");
            GameObject newMessage = Instantiate(messagePrefab, messageContainer.transform);
            PhoneMessageScript msgScript = newMessage.GetComponent<PhoneMessageScript>();

            if (currMsg < msgSenderIDs.Length)
            {
                msgScript.SetMsgProps(msgSenderIDs[currMsg], msgSizes[currMsg]);
                currMsg++;
            }

            msgScript.ClearText();
            dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            currMsgAnimator = msgScript.GetMsgAnimator();

            currMsgAnimator.SetBool("ShowMsg", true);
            nextMsg.GetComponent<DialogueTrigger>().TriggerDialogue();
            dialogueMan.DisplayNextSentence();

            msgShown = true;
        }
        else if(nextMsg.GetComponent<Image>() != null)
        {
            Debug.Log("Creating new photo message");
            GameObject newMessage = Instantiate(messagePhotoPrefab, messageContainer.transform);
            PhonePhotoScript msgScript = newMessage.GetComponent<PhonePhotoScript>();
            //dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            //currMsgAnimator = msgScript.GetMsgAnimator();
            msgShown = true;

            RectTransform msgContainerRect = messageContainer.GetComponent<RectTransform>();
            if (msgContainerRect != null && currMsg > 1)
            {
                msgContainerRect.offsetMax = new Vector2(msgContainerRect.offsetMax.x, msgContainerRect.offsetMax.y + 100);
                msgContainerRect.offsetMin = new Vector2(msgContainerBottom.x, msgContainerBottom.y);
            }

            msgScript.SetSprite(nextMsg.GetComponent<Image>().sprite);
        }
    }

    int GetSizeInc(int msgSize)
    {
        switch (msgSize)
        {
            case 0:
                return 20;
            case 1:
                return 30;
            case 2:
                return 40;
            default:
                return 25;
        }
    }

    void IncreaseMsgContainerSize(int size)
    {
        RectTransform msgContainerRect = messageContainer.GetComponent<RectTransform>();
        if (msgContainerRect != null)
        {
            switch (size)
            {
                case 0:
                    msgContainerRect.offsetMax = new Vector2(msgContainerRect.offsetMax.x, msgContainerRect.offsetMax.y + 20);
                    msgContainerRect.offsetMin = new Vector2(msgContainerBottom.x, msgContainerBottom.y);
                    break;
                case 1:
                    msgContainerRect.offsetMax = new Vector2(msgContainerRect.offsetMax.x, msgContainerRect.offsetMax.y + 30);
                    msgContainerRect.offsetMin = new Vector2(msgContainerBottom.x, msgContainerBottom.y);
                    break;
                case 2:
                    msgContainerRect.offsetMax = new Vector2(msgContainerRect.offsetMax.x, msgContainerRect.offsetMax.y + 40);
                    msgContainerRect.offsetMin = new Vector2(msgContainerBottom.x, msgContainerBottom.y);
                    break;
            }

        }
    }

    void LoadNextScene()
    {
        if(nextScene != "")
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    void Lv21Helper()
    {
        lv21cutscene.State5();
    }
}
