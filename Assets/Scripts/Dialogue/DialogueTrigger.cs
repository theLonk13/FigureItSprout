using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //condition set upon triggering this dialogue
    //if condition index is -1, then this dialogue triggers no condition
    //[SerializeField] int conditionTrigger = -1;
    //[SerializeField] NPCParent npc;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        /*
        if(conditionTrigger >= 0 && npc != null)
        {
            npc.UpdateCondition(conditionTrigger, 1);
        }
        */
    }
}
