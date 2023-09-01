using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour
{
    //rectTransform of the credits
    [SerializeField] RectTransform creditsRect;
    [SerializeField] Canvas canvas;

    //vector containing the original position of the credits
    Vector3 origin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        origin = creditsRect.anchoredPosition;
        //PSEUDOCODE
        //Fade out black screen
        //wait for a second or two on the game's logo
        //the image should be aligned with the top of the screen


        //then slowly move the credits up
        //should switch pivot of rectTransform and the anchorPoint of the recttransform to be the bottom center of the rectTransform
        //this will make the destination of the Lerp coroutine easy sicne it will be 0,0
        //when the image is done scrolling, auto go back to title screen

        Invoke("PrepToScroll", 2f);
    }

    void Update()
    {
        Debug.Log("Origin is " + origin);
        Debug.Log("Anchored Position of RectTransform: " + creditsRect.anchoredPosition);
        Debug.Log("Local Position of RectTransform: " + creditsRect.localPosition);

        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
        }
    }

    void PrepToScroll()
    {
        //setting anchor point to center bottom
        creditsRect.anchorMin = new Vector2(0.5f, 0f);
        Debug.Log("PosCP 1 : " + creditsRect.anchoredPosition);
        creditsRect.anchorMax = new Vector2(0.5f, 0f);
        Debug.Log("PosCP 2 : " + creditsRect.anchoredPosition);
        //set pivot to center bottom
        creditsRect.pivot = new Vector2(0.5f, 0f);
        creditsRect.anchoredPosition = new Vector2(0f, creditsRect.sizeDelta.y * -1f + canvas.GetComponent<RectTransform>().rect.height);
        Debug.Log("PosCP 3 : " + creditsRect.anchoredPosition);
        //save origin point
        origin = creditsRect.anchoredPosition;

        StartCoroutine(ScrollCredits());
    }

    IEnumerator ScrollCredits()
    {
        float totalMovementTime = 40f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        while (Vector3.Distance(creditsRect.anchoredPosition, Vector3.zero) > 0)
        {
            currentMovementTime += Time.deltaTime;
            creditsRect.anchoredPosition = Vector3.Lerp(origin, Vector3.zero, currentMovementTime / totalMovementTime);
            yield return null;
        }

        //Invoke("GoToTitle", 2f);
    }

    public void GoToTitle()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("TitleScreen");
    }
}
