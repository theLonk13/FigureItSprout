using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lv2OpenBook : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject tutorialOverlay;
    [SerializeField] BookController bookController;
    [SerializeField] GameObject nextScreen;

    [SerializeField] int openToPlant = 2;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            openBook();    
        }
    }

    void openBook()
    {
        //Hard Code open to sage
        bookController.OpenToPlant(openToPlant);
        Destroy(tutorialOverlay);
        nextScreen.SetActive(true);
    }
}
