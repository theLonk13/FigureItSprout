using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int plantID = 0; //type of plant this script is attached to
    
    //static fields for use by other scripts
    public static GameObject LastCardSlot; //the last pot tile the card was dragged over
    public static int plantDrag = 0; //type of plant being dragged
    public static bool dragging; //true if currently dragging a card
    public static GameObject playerHand; //the deck holder

    private Image image; //used to turn raycast targetting off when dragging

    void Awake()
    {
        image = GetComponent<Image>();
        playerHand = GameObject.Find("PlayerHandArea");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        transform.SetParent(transform.root);
        image.raycastTarget = false;
        plantDrag = plantID;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        transform.SetParent(playerHand.transform);
        image.raycastTarget = true;

        LevelTile plot_tile = LastCardSlot.GetComponent<LevelTile>();
        if (plot_tile != null && plot_tile.getPlantType() <= 0)
        {
            plot_tile.plantPlant(plantID);
            Destroy(this.gameObject);
        }
    }

}
