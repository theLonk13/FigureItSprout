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

    //FollowMouseScript
    private FollowMouseScript followMouse = null;

    //tracks if the mouse is over this card
    bool hoverThis;
    [SerializeField] bool playableCard;

    void Awake()
    {
        followMouse = GameObject.Find("FollowMouse").GetComponent<FollowMouseScript>();
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
        followMouse.IncHover(plantID);
        hoverThis = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        followMouse.DecHover();
        hoverThis = false;
    }

    public int getPlantID()
    {
        return plantID;
    }

    public bool getHoverThis()
    {
        return hoverThis;
    }
}
