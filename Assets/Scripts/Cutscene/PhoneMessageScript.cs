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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
