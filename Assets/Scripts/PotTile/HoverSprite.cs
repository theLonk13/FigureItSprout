using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject hoverSpriteObj;
    LevelTile thisTile;
    Image hoverSprite;
    PottedSpriteInfo potted_sprites;

    void Awake()
    {
        thisTile = this.gameObject.GetComponent<LevelTile>();
        potted_sprites = GameObject.FindWithTag("PottedSprite").GetComponent<PottedSpriteInfo>();
        hoverSprite = hoverSpriteObj.GetComponent<Image>();
        if (hoverSprite == null) { Debug.LogError("hoverSprite failed to initialize"); }
    }

    void Update()
    {
        if (!(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))) { hoverSprite.enabled = false; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DragAndDrop.dragging && thisTile.getPlantType() <= 0)
        {
            Debug.Log("Entered Pot");
            hoverSprite.sprite = potted_sprites.get_potted_sprite(DragAndDrop.plantDrag);
            hoverSprite.enabled = true;

            if(DragAndDrop.dragImage != null)
            {
                DragAndDrop.dragImage.enabled = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Pot");
        hoverSprite.enabled = false;

        if(DragAndDrop.dragImage != null)
        {
            DragAndDrop.dragImage.enabled = true;
        }
    }
}