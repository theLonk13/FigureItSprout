using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOverlay : MonoBehaviour
{
    //data about the actual tutorial screens
    [SerializeField] GameObject screens;
    [SerializeField] int numScreens;

    int curr_screen;

    //Holds a new card screen object if one should be displayed after tutorial is done
    [SerializeField] GameObject newCard;

    // Start is called before the first frame update
    void Start()
    {
        curr_screen = 0;
    }

    // Update is called once per frame
    void Update()
    {
        screens.GetComponent<RectTransform>().localPosition = new Vector3(
            -1000 * curr_screen,
            0,
            0
        );
    }

    public void Back()
    {
        if (curr_screen > 0) curr_screen--;
    }

    public void Next()
    {
        if (curr_screen >= numScreens - 1) { ShowNewCard(); }
        else { curr_screen++; }
    }

    void ShowNewCard()
    {
        if(newCard != null) newCard.SetActive(true);
        Destroy(this.gameObject);
    }
}
