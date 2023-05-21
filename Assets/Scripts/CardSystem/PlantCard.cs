using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int plantID;

    public static GameObject LastCardSlot;
    public static GameObject playerHand;
    private Image image;

    //tracks if the mouse is over this card
    bool hoverThis;

    void Awake()
    {
        image = GetComponent<Image>();
        playerHand = GameObject.Find("PlayerHandArea");
        hoverThis = false;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(playerHand.transform);
        image.raycastTarget = true;

        LevelTile plot_tile = LastCardSlot.GetComponent<LevelTile>();
        if (plot_tile != null && plot_tile.getPlantType() <= 0)
        {
            plot_tile.plantPlant(plantID);
            Destroy(this.gameObject);
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
}
