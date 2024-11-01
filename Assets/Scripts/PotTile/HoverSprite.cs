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

    LevelManager lvMan;

    void Awake()
    {
        thisTile = this.gameObject.GetComponent<LevelTile>();
        potted_sprites = GameObject.Find("DayPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        hoverSprite = hoverSpriteObj.GetComponent<Image>();
        if (hoverSprite == null) { Debug.LogError("hoverSprite failed to initialize"); }

        GameObject LevelManObj = GameObject.Find("LevelUICanvas");
        if(LevelManObj != null)
        {
            lvMan = LevelManObj.GetComponent<LevelManager>();
        }
    }

    void Update()
    {
        if (!(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))) { hoverSprite.enabled = false; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Entered Pot");
        if (DragAndDrop.dragging && thisTile.getPlantType() <= 0)
        {
            hoverSprite.sprite = potted_sprites.get_potted_sprite(DragAndDrop.plantDrag);
            hoverSprite.enabled = true;

            if(DragAndDrop.dragImage != null)
            {
                DragAndDrop.dragImage.enabled = false;
            }

            if (lvMan != null) { lvMan.HoverAOE(DragAndDrop.plantDrag, thisTile.GetTileColumn(), thisTile.GetTileRow()); }
        }

        thisTile.hoverThis = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit Pot");
        hoverSprite.enabled = false;

        if(DragAndDrop.dragImage != null)
        {
            DragAndDrop.dragImage.enabled = true;
        }

        if (lvMan != null) { lvMan.HoverAOE(); }

        thisTile.hoverThis = false;
    }
}
