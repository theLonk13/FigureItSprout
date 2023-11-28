using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneMessageScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Animator messageAnim;
    [SerializeField] Image messageBG;

    //sprites for the message bubble for self and others
    [SerializeField] Sprite selfMsgSmall;
    [SerializeField] Sprite selfMsgMed;
    [SerializeField] Sprite selfMsgBig;
    [SerializeField] Sprite otherMsgSmall;
    [SerializeField] Sprite otherMsgMed;
    [SerializeField] Sprite otherMsgBig;

    [SerializeField] float smallSize; // size 0
    [SerializeField] float medSize; // size 1
    [SerializeField] float bigSize; // size 2

    //colors for different ppl
    [SerializeField] Color maggieColor; //id 0
    [SerializeField] Color momColor; //id 1
    [SerializeField] Color dadColor; //id 2
    [SerializeField] Color cassColor; //id 3

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMsgProps(int id, int size)
    {
        //Debug.Log("Setting msg sender to id : " + id);
        Debug.Log(messageBG.sprite);
        switch (id)
        {
            case 0:
                messageBG.color = maggieColor;
                break;
            case 1:
                messageBG.color = momColor;
                break;
            case 2:
                messageBG.color = dadColor;
                break;
            case 3:
                messageBG.color = cassColor;
                break;
            default:
                messageBG.color = maggieColor;
                break;
        }
        if(id == 0)
        {
            switch (size)
            {
                case 0:
                    messageBG.sprite = selfMsgSmall;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, smallSize);
                    break;
                case 1:
                    messageBG.sprite = selfMsgMed;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, medSize);
                    break;
                case 2:
                    messageBG.sprite = selfMsgBig;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, bigSize);
                    break;
            }
            messageText.alignment = TextAlignmentOptions.Right;
        }
        else
        {
            switch (size)
            {
                case 0:
                    messageBG.sprite = otherMsgSmall;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, smallSize);
                    break;
                case 1:
                    messageBG.sprite = otherMsgMed;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, medSize);
                    break;
                case 2:
                    messageBG.sprite = otherMsgBig;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, bigSize);
                    break;
            }
            messageText.alignment = TextAlignmentOptions.Left;
        }
        Debug.Log(messageBG.sprite);
    }

    public TextMeshProUGUI GetMessageBox()
    {
        return messageText;
    }

    public Animator GetMsgAnimator()
    {
        return messageAnim;
    }

    public Image GetMsgBG()
    {
        return messageBG;
    }

    public void ClearText()
    {
        messageText.text = "";
    }
}
