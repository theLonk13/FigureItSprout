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

    //timing variables
    [SerializeField] float showPlantDelay;
    float showPlantTimer = 0f;

    PlantCardSprites cardSprites;

    GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        cardSprites = GameObject.Find("PlantCardSprites").GetComponent<PlantCardSprites>();

        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10) ;
        followMouse.position = Vector3.MoveTowards(followMouse.position, mousePosition, moveSpeed * Time.deltaTime);

        CheckHover();
        UpdateShowTimer();
        ToggleShowPlant(showPlant);

        /*
        if(numHover == 0)
        {
            ToggleShowPlant(false);
        }
        */
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
        if(!show || showPlantTimer >= showPlantDelay)
        {
            followMouseAnim.SetBool("ShowPlant", showPlant);
        }
    }

    void CheckHover()
    {
        bool currHover = false;
        foreach (GameObject tile in tiles)
        {
            if (tile != null)
            {
                LevelTile tiledata = tile.GetComponent<LevelTile>();
                if (tiledata != null && tiledata.hoverThis && tiledata.getPlantType() != -1 && !Input.GetMouseButton(0))
                {
                    currHover = true;
                }
            }
        }

        showPlant = currHover;
    }

    void UpdateShowTimer()
    {
        if(!showPlant && showPlantTimer > 0f)
        {
            showPlantTimer -= Time.deltaTime;
        }
        else
        {
            showPlantTimer += Time.deltaTime;
        }
    }
}
