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

    void Awake()
    {
        hoverThis = false;
        startIdle = GameObject.Find("UISprites").GetComponent<UISprites>().getStartIdle();
        startHighlight = GameObject.Find("UISprites").GetComponent<UISprites>().getStartHighlight();
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
        SceneManager.LoadScene("Act1Lv1");
    }
}
