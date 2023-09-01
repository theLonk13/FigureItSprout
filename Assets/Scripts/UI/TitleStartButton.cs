using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleStartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Sprite startIdle;
    Sprite startHighlight;

    [SerializeField] Image startButton;
    bool hoverThis;
    bool fadeInButton = false;

    //logo
    [SerializeField] RectTransform logoRect;
    Vector3 origLogoPos;
    float timeStamp = 0.0f;

    [SerializeField] Image logoImg;
    bool fadeIn = false;
    //Audio for button click
    AudioSource buttonAudio;

    void Awake()
    {
        hoverThis = false;
        startIdle = GameObject.Find("UISprites").GetComponent<UISprites>().getStartIdle();
        startHighlight = GameObject.Find("UISprites").GetComponent<UISprites>().getStartHighlight();
        buttonAudio = GetComponent<AudioSource>();
        if(logoRect != null )
        {
            origLogoPos = logoRect.localPosition;
        }
        if(logoImg != null )
        {
            Invoke("FadeInLogo", 1f);
        }
        if(startButton != null)
        {
            startButton.enabled = false;
            Invoke("FadeInButton", 2f);
        }

    }

    void Update()
    {
        /*
        if (hoverThis)
        {
            startButton.sprite = startHighlight;
        }
        else
        {
            startButton.sprite = startIdle;
        }
        */

        if(logoRect != null)
        {
            timeStamp += 1.5f * Time.deltaTime;
            Vector3 newPos = origLogoPos;
            newPos.y = newPos.y + 15f * Mathf.Sin(timeStamp);
            logoRect.localPosition = newPos;
        }

        if(fadeIn && logoImg != null)
        {
            Color temp = logoImg.color;
            temp.a += 4f * Time.deltaTime;
            logoImg.color = temp;
        }

        if(fadeInButton && startButton != null)
        {
            Color temp2 = startButton.color;
            temp2.a += 4f * Time.deltaTime;
            startButton.color = temp2;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverThis = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverThis = false;
    }

    public void StartGame()
    {
        buttonAudio.Play();
        LevelData levelData = GameObject.Find("LevelData").GetComponent<LevelData>();

        if(levelData != null && levelData.get_level_data()[0] == 1)
        {
            SceneManager.LoadScene("LevelSelect");
            return;
        }
        SceneManager.LoadScene("IntroCutscene");
    }

    void FadeInLogo()
    {
        fadeIn = true;
    }

    void FadeInButton()
    {
        fadeInButton = true;
        startButton.enabled = true;
    }

    //These methods are for other Title Screen UI elements that did not necessarily require an additional script
    public void GoToBonus()
    {
        buttonAudio.Play();
        SceneManager.LoadScene("BonusRewards");
    }

    public void GoToCredits()
    {
        buttonAudio.Play();
        SceneManager.LoadScene("Credits");
    }

    public void quit()
    {
        Application.Quit();
    }
}
