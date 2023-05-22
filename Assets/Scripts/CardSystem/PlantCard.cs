using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int plantID;

    public static GameObject LastCardSlot;
    public static GameObject playerHand;
    private Image image;

    //tracks if the mouse is over this card
    bool hoverThis;
    [SerializeField] bool playableCard;

    void Awake()
    {
        hoverThis = false;
        if (!playableCard)
        {
            DragAndDrop drag = GetComponent<DragAndDrop>();
            if (drag != null)
            {
                drag.enabled = false;
            }
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && hoverThis)
        {
            BookController book = GameObject.Find("BookPages").GetComponent<BookController>();
            if(book != null)
            {
                book.OpenToPlant(plantID);
            }
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

    public int getPlantID()
    {
        return plantID;
    }
}
