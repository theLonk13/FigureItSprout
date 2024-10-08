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
    GameObject[] plantCards;

    // Start is called before the first frame update
    void Start()
    {
        cardSprites = GameObject.Find("PlantCardSprites").GetComponent<PlantCardSprites>();

        tiles = GameObject.FindGameObjectsWithTag("Tile");
        plantCards = GameObject.FindGameObjectsWithTag("PlantCard");
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;
        xPos = Mathf.Clamp(xPos, 2*followMouse.rect.width, Screen.width);
        yPos = Mathf.Clamp(yPos, 0f, Screen.height - 2*followMouse.rect.height);
        //xPos = Mathf.Clamp(xPos, 350, 450);
        
        Vector3 mousePosition = new Vector3(xPos, yPos, -10) ;
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
            Debug.Log("FollowMouse now showing plant ID : " + plantID);
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
        if(numHover <= 0)
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
                    break;
                }
            }
        }

        foreach(GameObject card in plantCards)
        {
            if (currHover) { break; }

            if(card != null)
            {
                PlantCard plantCardScript = card.GetComponent<PlantCard>();
                if(plantCardScript != null && plantCardScript.getHoverThis() && !Input.GetMouseButton(0))
                {
                    currHover= true; break;
                }
            }
        }

        if (currHover != showPlant)
        {
            showPlant = currHover;
            ToggleShowPlant(currHover);
        }
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
