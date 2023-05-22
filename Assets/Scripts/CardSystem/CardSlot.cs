using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        DragAndDrop.LastCardSlot = this.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragAndDrop.LastCardSlot = GameObject.Find("PlayerHandArea");
    }
}
