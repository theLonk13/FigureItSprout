using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsScreen2 : MonoBehaviour
{
    bool autoScroll = false;

    [SerializeField] float speed = 1.0f;

    //rectTransform of the credits
    [SerializeField] RectTransform creditsRect;
    //[SerializeField] RectTransform canvasRect;
    [SerializeField] Scrollbar scroll;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AutoScrollOn", 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("creditsRect.sizeDelta.y is " + creditsRect.sizeDelta.y);
        //Debug.Log("creditsRect.rect.height is " + creditsRect.rect.height);

        //Debug.Log("canvas.sizeDelta.y is " + canvasRect.sizeDelta.y);
        //Debug.Log("canvas.sizeDelta.y / scaleFactor is " + canvasRect.sizeDelta.y / canvasRect.GetCanvas().scaleFactor);

        if (Input.GetMouseButtonDown(0))
        {
            autoScroll = false;
        }
        else if (autoScroll && scroll.value > 0)
        {
            creditsRect.anchoredPosition = new Vector2(0f, creditsRect.anchoredPosition.y + speed * Time.deltaTime);
        }
        else
        {
            autoScroll = false;
        }
    }

    void AutoScrollOn()
    {
        autoScroll = true;

        /*
        //set up the new anchor point
        //setting anchor point to center bottom
        creditsRect.anchorMin = new Vector2(0.5f, 0f);
        Debug.Log("PosCP 1 : " + creditsRect.anchoredPosition);
        creditsRect.anchorMax = new Vector2(0.5f, 0f);
        Debug.Log("PosCP 2 : " + creditsRect.anchoredPosition);
        //set pivot to center bottom
        creditsRect.pivot = new Vector2(0.5f, 0f);
        creditsRect.anchoredPosition = new Vector2(0f, creditsRect.sizeDelta.y * -1f + canvas.GetComponent<RectTransform>().rect.height);
        Debug.Log("PosCP 3 : " + creditsRect.anchoredPosition);
        */
    }

    public void GoToTitle()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("TitleScreen");
    }
}
