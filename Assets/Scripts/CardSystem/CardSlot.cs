using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlantCard.LastCardSlot = this.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlantCard.LastCardSlot = GameObject.Find("PlayerHandArea");
    }
}
