using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv21Phone : MonoBehaviour
{
    //flag for if this phone has rung
    public static bool rang = false;

    //Gameobject for phone
    [SerializeField] GameObject phone;
    RectTransform phoneTransform;
    //Image for the phone
    Image phoneImage;

    //delay before call
    [SerializeField] float callDelay = 20;
    //sprites for various stages of the call
    [SerializeField] Sprite phoneSilent;
    [SerializeField] Sprite phoneCall;
    [SerializeField] Sprite phoneCallDecline;

    [SerializeField] Image phoneCallDeclineImage;

    //switch for vibration
    bool vibrate = false;
    //vibration audio
    AudioSource vibrateAudio;
    //vibration parameters
    [SerializeField] float speed = 1.0f;
    [SerializeField] float amount = 1.0f;
    //original location of phone
    float origX;

    //calling flag
    bool calling = false;
    //decline flag
    bool declined = false;
    //sleep flag
    bool sleep = false;

    // Start is called before the first frame update
    void Start()
    {
        if (rang) { return; }
        phoneTransform = phone.GetComponent<RectTransform>();
        phoneImage = phone.GetComponent<Image>();
        vibrateAudio = GetComponent<AudioSource>();
        origX = phoneTransform.localPosition.x;
        Invoke("PhoneCall", callDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (vibrate)
        {
            phoneTransform.localPosition = new Vector3(
                Mathf.Sin(Time.time * speed) * amount,
                phoneTransform.localPosition.y,
                phoneTransform.localPosition.z);
        }

        if (sleep)
        {
            Color tempColor = phoneCallDeclineImage.color;
            tempColor.a = Mathf.Max(tempColor.a - .02f, 0f);
            phoneCallDeclineImage.color = tempColor;
        }
    }

    //start phone call
    public void PhoneCall()
    {
        if (!calling)
        {
            CancelInvoke();
            phoneCallDeclineImage.enabled = false;
            calling = true;
            declined = false;
            phoneImage.sprite = phoneCall;
            VibrateOn();
        }
    }

    public void PhoneCall(bool cutscene = false)
    {
        Debug.Log("Attempting to call");
        if (cutscene)
        {
            phoneImage.raycastTarget = false;
        }
        else
        {
            phoneImage.raycastTarget = true;
        }

        if (!calling)
        {
            CancelInvoke();
            phoneCallDeclineImage.enabled = false;
            calling = true;
            declined = false;
            phoneImage.sprite = phoneCall;
            VibrateOn();
        }
    }

    void VibrateOn()
    {
        if (declined) return;
        vibrate = true;
        vibrateAudio.Play();
        Invoke("VibrateOff", 1);
    }

    void VibrateOff()
    {
        vibrate = false;
        if(vibrateAudio.isPlaying) { vibrateAudio.Stop(); }
        phoneTransform.localPosition = new Vector3(
                origX,
                phoneTransform.localPosition.y,
                phoneTransform.localPosition.z);
        if (declined) return;
        Invoke("VibrateOn", 2);
    }

    //this should be called when the player clicks on the phone after it starts calling
    public void DeclineCall()
    {
        if(!calling) return;
        declined = true;
        calling = false;
        rang = true;
        VibrateOff();
        phoneImage.sprite = phoneCallDecline;
        phoneCallDeclineImage.enabled = true;
        Invoke("PhoneOff", 5);
    }

    public void DelayedDecline()
    {
        declined = true;
    }

    void PhoneOff()
    {
        phoneImage.sprite = phoneSilent;
        sleep = true;
    }
}
