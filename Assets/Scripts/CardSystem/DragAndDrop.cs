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
    public static Image dragImage; //Image component attached to the card currently being dragged

    private Image image; //used to turn raycast targetting off when dragging
    private int lastSiblingIndex;
    private float lastX;

    void Awake()
    {
        image = GetComponent<Image>();
        playerHand = GameObject.Find("PlayerHandArea");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //info for putting card back in right spot
        lastSiblingIndex = transform.GetSiblingIndex();
        lastX = eventData.position.x;

        //allow for smooth dragging
        dragging = true;
        transform.SetParent(transform.root);
        image.raycastTarget = false;

        //for faded sprite on pots when hovering over
        plantDrag = plantID;
        dragImage = GetComponent<Image>();

        //debug info
        Debug.Log("LastX : " + lastX + "\tLastSiblingIndex : " + lastSiblingIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //calculate correct place to put card before adding to parent
        CalcSiblingIndex(eventData.position.x, eventData.position.y);

        dragging = false;
        transform.SetParent(playerHand.transform);
        image.raycastTarget = true;

        if(LastCardSlot != null)
        {
            LevelTile plot_tile = LastCardSlot.GetComponent<LevelTile>();
            if (plot_tile != null && plot_tile.getPlantType() <= 0)
            {
                plot_tile.plantPlant(plantID);
                Destroy(this.gameObject);
                return;
            }
        }

        //place card in correct spot
        transform.SetSiblingIndex(lastSiblingIndex);
    }

    //calculate new sibling index based on position of card when dragging stops and number of cards present
    void CalcSiblingIndex(float eventX, float eventY)
    {
        if(eventY > 140) { return; }

        //calc based on amount moved
        //innaccurate
        /*
        int calc_index = lastSiblingIndex;
        calc_index = lastSiblingIndex + (int)(eventX - lastX) / 80;
        if(calc_index < 0) { calc_index = 0; }

        int numCards = GameObject.FindGameObjectsWithTag("PlantCard").Length;
        if (calc_index >= numCards) { calc_index = numCards - 1; }

        lastSiblingIndex = calc_index;
        */

        //check all other plant cards for relative position
        GameObject[] cards = GameObject.FindGameObjectsWithTag("PlantCard");
        int right = cards.Length - 1;
        int left = 0;
        foreach(GameObject card in cards)
        {
            Transform cardTransform = card.GetComponent<Transform>();
            if (cardTransform != null && cardTransform != this.transform)
            {
                if(cardTransform.position.x < eventX && cardTransform.GetSiblingIndex() >= left)
                {
                    left = cardTransform.GetSiblingIndex() + 1;
                }
                else if(cardTransform.position.x > eventX && cardTransform.GetSiblingIndex()  < right)
                {
                    right = cardTransform.GetSiblingIndex();
                }
            }
        }

        if (left == right) { lastSiblingIndex = left; }

        //debug info
        Debug.Log("EventData X: " + eventX + "\tEventData Y: " + eventY + "\tLeft: " + left + "\tRight: " + right + "\tLastSiblingIndex: " + lastSiblingIndex);
    }
}
