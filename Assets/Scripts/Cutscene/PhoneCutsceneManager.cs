using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCutsceneManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueMan;
    [SerializeField] DialogueTrigger dialogueStart;

    [SerializeField] GameObject messageContainer;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject messagePhotoPrefab;

    [SerializeField] Animator currMsgAnimator;

    [SerializeField] DialogueTrigger[] dialogueTriggers;
    int nextDialogueTrigger = 0;
    [SerializeField] Sprite[] photoMsgs;
    int nextPhotoMsg = 0;
    bool photoNext = false;

    [SerializeField] GameObject[] messageQueue;
    int nextMessage = 0;

    bool msgShown = false;
    // Start is called before the first frame update
    void Start()
    {
        //dialogueStart.TriggerDialogue();
        GetNextMessage();
        currMsgAnimator.SetBool("ShowMsg", true);
        msgShown = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceDialogue()
    {
        if(dialogueMan != null && dialogueMan.CheckCurrDialogue())
        {
            dialogueMan.DisplayNextSentence();
        }
        else if(!msgShown){
            if(currMsgAnimator != null)
            {
                currMsgAnimator.SetBool("ShowMsg", true);
            }
            dialogueMan.DisplayNextSentence();
            msgShown = true;
        }
        else if(dialogueMan.CheckSentencesLeft() != 0)
        {
            Debug.Log("Continuing current dialogue");
            GameObject newMessage = Instantiate(messagePrefab, messageContainer.transform);
            PhoneMessageScript msgScript = newMessage.GetComponent<PhoneMessageScript>();
            dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            currMsgAnimator = msgScript.GetMsgAnimator();
            msgShown = false;
        }else
        {
            GetNextMessage() ;
        }
    }

    void GetNextMessage()
    {
        if(nextMessage >= messageQueue.Length) { return; }

        GameObject nextMsg = messageQueue[nextMessage];
        nextMessage++;

        if(nextMsg.GetComponent<DialogueTrigger>() != null)
        {
            Debug.Log("Creating new text message");
            GameObject newMessage = Instantiate(messagePrefab, messageContainer.transform);
            PhoneMessageScript msgScript = newMessage.GetComponent<PhoneMessageScript>();
            msgScript.ClearText();
            dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            currMsgAnimator = msgScript.GetMsgAnimator();
            msgShown = false;

            nextMsg.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if(nextMsg.GetComponent<Image>() != null)
        {
            Debug.Log("Creating new photo message");
            GameObject newMessage = Instantiate(messagePhotoPrefab, messageContainer.transform);
            PhonePhotoScript msgScript = newMessage.GetComponent<PhonePhotoScript>();
            //dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            //currMsgAnimator = msgScript.GetMsgAnimator();
            msgShown = false;

            msgScript.SetSprite(nextMsg.GetComponent<Image>().sprite);
        }
    }
}
