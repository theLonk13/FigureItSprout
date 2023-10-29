using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCutsceneManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueMan;
    [SerializeField] DialogueTrigger dialogueStart;

    [SerializeField] GameObject messageContainer;
    [SerializeField] GameObject messagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        dialogueStart.TriggerDialogue();    
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
        else if(dialogueMan.CheckSentencesLeft() != 0)
        {
            GameObject newMessage = Instantiate(messagePrefab, messageContainer.transform);
            PhoneMessageScript msgScript = newMessage.GetComponent<PhoneMessageScript>();
            dialogueMan.SetDialogueBox(msgScript.GetMessageBox());
            dialogueMan.DisplayNextSentence();
        }
    }
}
