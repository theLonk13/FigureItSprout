using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //textboxes for dialogue
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    //animator to move dialogue box into and out of screen
    public Animator animator;

    //queue of sentences
    private Queue<string> sentences;
    string currSentence = "";

    //slime player
    //private SlimeState slime;

    //current dialogue coroutine
    IEnumerator currDialogue;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        //slime = GameObject.Find("Slime").GetComponent<SlimeState>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);
        //animator.SetBool("IsOpen", true);
        //slime.ToggleInDialogue(true);

        if(sentences == null) { sentences = new Queue<string>(); }
        sentences.Clear();
        //nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currDialogue != null)
        {
            StopCoroutine(currDialogue); //stop current dialogue coroutine
            dialogueText.text = currSentence; //auto fill the rest of the sentence
            currDialogue = null;
        }
        else
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }


            if (currDialogue != null) { StopCoroutine(currDialogue); } //stop current dialogue coroutine
            currSentence = sentences.Dequeue();
            currDialogue = TypeSentence(currSentence); //set curr dialogue to a new coroutine with new sentence
            StartCoroutine(currDialogue); //start a new dialogue coroutine with a new sentence
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        currDialogue = null;
    }

    void EndDialogue()
    {
        //Debug.Log("End of conversation");
        //animator.SetBool("IsOpen", false);


        //slime.ToggleInDialogue(false);

        /*
        GameObject mainCam = GameObject.Find("Main Camera");
        if (mainCam != null)
        {
            mainCam.transform.parent = slime.gameObject.transform;
            //mainCam.transform.localPosition = new Vector3(0f, 0f, -16f);
            MainCamScript camScript = mainCam.GetComponent<MainCamScript>();
            if (camScript != null)
            {
                camScript.ResetTransform();
            }
        }
        */
    }

    public int CheckSentencesLeft()
    {
        return sentences.Count;
    }

    public bool CheckCurrDialogue()
    {
        return currDialogue != null;
    }

    public void SetDialogueBox(TextMeshProUGUI newTextBox)
    {
        dialogueText = newTextBox;
    }
}
