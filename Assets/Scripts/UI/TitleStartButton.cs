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

    //Audio for button click
    AudioSource buttonAudio;

    void Awake()
    {
        hoverThis = false;
        startIdle = GameObject.Find("UISprites").GetComponent<UISprites>().getStartIdle();
        startHighlight = GameObject.Find("UISprites").GetComponent<UISprites>().getStartHighlight();
        buttonAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hoverThis)
        {
            startButton.sprite = startHighlight;
        }
        else
        {
            startButton.sprite = startIdle;
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
        SceneManager.LoadScene("IntroCutscene");
        //SceneManager.LoadScene("Act1Lv1");
    }

    //These methods are for other Title Screen UI elements that did not necessarily require an additional script
    public void GoToBonus()
    {
        buttonAudio.Play();
        SceneManager.LoadScene("BonusRewards");
    }
}
