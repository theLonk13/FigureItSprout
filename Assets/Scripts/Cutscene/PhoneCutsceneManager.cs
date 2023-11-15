using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCutsceneManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueMan;
    [SerializeField] DialogueTrigger dialogueStart;

    [SerializeField] GameObject messageContainer;
    [SerializeField] GameObject messagePrefab;

    [SerializeField] Animator currMsgAnimator;

    bool msgShown = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogueStart.TriggerDialogue();
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
            GameObject newMessage = Instantiate(messagePrefab, messageContainer.transform);
            PhoneMessageScript msgScript = newMessage.GetComponent<PhoneMessageScript>();
            dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            currMsgAnimator = msgScript.GetMsgAnimator();
            msgShown = false;
        }
    }
}
