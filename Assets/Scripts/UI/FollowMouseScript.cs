using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouseScript : MonoBehaviour
{
    [SerializeField] RectTransform followMouse;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] Animator followMouseAnim;
    bool showPlant = false;
    int numHover = 0;
    [SerializeField] Image plantImage;

    PlantCardSprites cardSprites;

    // Start is called before the first frame update
    void Start()
    {
        cardSprites = GameObject.Find("PlantCardSprites").GetComponent<PlantCardSprites>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10) ;
        followMouse.position = Vector3.MoveTowards(followMouse.position, mousePosition, moveSpeed * Time.deltaTime);

        if(numHover == 0)
        {
            ToggleShowPlant(false);
        }
    }

    public void SetPlant(int plantID)
    {
        if(plantImage != null && cardSprites != null)
        {
            if(plantID > 0)
            {
                plantImage.sprite = cardSprites.GetPlantCardSprite(plantID);
                ToggleShowPlant(true);
            }
            else
            {
                ToggleShowPlant(false);
            }
        }

        if(Input.GetMouseButton(0))
        {
            ToggleShowPlant(false);
        }
    }

    public void IncHover(int plantID)
    {
        if (plantID > 0)
        {
            ToggleShowPlant(true);
        }
        else
        {
            ToggleShowPlant(false);
        }
        SetPlant(plantID);
        numHover++;
    }

    public void DecHover()
    {
        numHover--;
        if(numHover == 0)
        {
            ToggleShowPlant(false);
        }
    }

    public void ToggleShowPlant(bool show)
    {
        showPlant = show;
        followMouseAnim.SetBool("ShowPlant", showPlant);
    }
}
