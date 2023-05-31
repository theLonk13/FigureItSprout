using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite idle;
    [SerializeField] Sprite highlight;

    Image image;
    bool hover;

    // Start is called before the first frame update
    void Start()
    {
        hover = false;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hover) { image.sprite = highlight; }
        else { image.sprite = idle; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
}
