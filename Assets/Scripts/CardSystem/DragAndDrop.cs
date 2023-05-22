using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int plantID = 0;

    public static GameObject LastCardSlot;
    public static GameObject playerHand;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        playerHand = GameObject.Find("PlayerHandArea");
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

}
