using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneMessageScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Animator messageAnim;
    [SerializeField] Image messageBG;

    //sprites for the message bubble for self and others
    [SerializeField] Sprite selfMsgSmall;
    [SerializeField] Sprite selfMsgMed;
    [SerializeField] Sprite selfMsgBig;
    [SerializeField] Sprite selfMsg4;
    [SerializeField] Sprite selfMsg5;
    [SerializeField] Sprite selfMsg6;
    [SerializeField] Sprite selfMsg7;
    [SerializeField] Sprite selfMsg8;
    [SerializeField] Sprite selfMsg9;
    [SerializeField] Sprite otherMsgSmall;
    [SerializeField] Sprite otherMsgMed;
    [SerializeField] Sprite otherMsgBig;
    [SerializeField] Sprite otherMsg4;
    [SerializeField] Sprite otherMsg5;
    [SerializeField] Sprite otherMsg6;
    [SerializeField] Sprite otherMsg7;
    [SerializeField] Sprite otherMsg8;
    [SerializeField] Sprite otherMsg9;

    [SerializeField] float smallSize; // size 0
    [SerializeField] float medSize; // size 1
    [SerializeField] float bigSize; // size 2
    [SerializeField] float size4;
    [SerializeField] float size5;
    [SerializeField] float size6;
    [SerializeField] float size7;
    [SerializeField] float size8;
    [SerializeField] float size9;

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

    public void SetMsgProps(int id, int size, bool newSender)
    {
        //Debug.Log("Setting msg sender to id : " + id);
        //Debug.Log(messageBG.sprite);
        if (!newSender)
        {
            nameText.enabled = false;
        }

        switch (id)
        {
            case 0:
                messageBG.color = maggieColor;
                nameText.text = "";
                break;
            case 1:
                messageBG.color = momColor;
                nameText.text = "Mom";
                break;
            case 2:
                messageBG.color = dadColor;
                nameText.text = "Dad";
                break;
            case 3:
                messageBG.color = cassColor;
                nameText.text = "Cassandra";
                break;
            default:
                messageBG.color = maggieColor;
                nameText.text = "";
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
                case 3:
                    messageBG.sprite = selfMsg4;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size4);
                    break;
                case 4:
                    messageBG.sprite = selfMsg5;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size5);
                    break;
                case 5:
                    messageBG.sprite = selfMsg6;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size6);
                    break;
                case 6:
                    messageBG.sprite = selfMsg7;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size7);
                    break;
                case 7:
                    messageBG.sprite = selfMsg8;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size8);
                    break;
                case 8:
                    messageBG.sprite = selfMsg9;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size9);
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
                case 3:
                    messageBG.sprite = otherMsg4;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size4);
                    break;
                case 4:
                    messageBG.sprite = otherMsg5;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size5);
                    break;
                case 5:
                    messageBG.sprite = otherMsg6;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size6);
                    break;
                case 6:
                    messageBG.sprite = otherMsg7;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size7);
                    break;
                case 7:
                    messageBG.sprite = otherMsg8;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size8);
                    break;
                case 8:
                    messageBG.sprite = otherMsg9;
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, size9);
                    break;
            }
            messageText.alignment = TextAlignmentOptions.Left;
        }
        //Debug.Log(messageBG.sprite);
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
