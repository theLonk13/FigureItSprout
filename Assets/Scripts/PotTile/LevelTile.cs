using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LevelTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int row, col, curr_score;
    int plantType, turnCounter;

    //GameObject plant;       //tracks the plant currently planted on this tile
    LevelManager levelManager;

    //persistent script that holds the potted sprites
    PottedSpriteInfo potted_sprites;

    //Image rendering this pot
    [SerializeField] GameObject potImageObj;
    RectTransform potImageRect;
    Image potImage;
    //Empty tile image
    Image tileImage;

    //Pot Audio
    [SerializeField] PotAudio potAudio;
    //unpotted plant flag
    bool Unpotted = false;

    //Indicator for shy plant activation
    int shy_toggle = -1;
    [SerializeField] GameObject shy_indicator;

    //Indicator for blossoming plant counter
    [SerializeField] GameObject blossom_indicator;
    [SerializeField] TextMeshProUGUI blossom_text;

    //Score object
    [SerializeField] GameObject scoreDisplay;

    //animator for tile
    Animator animator;
    //falling pot scale
    float fallScale = 1f;
    //falling flag
    bool falling = false;

    //animator for point change sprites
    [SerializeField] Animator pointChangeAnimator;
    //animator for pot
    [SerializeField] Animator potAnimator;
    //particle system for lv complete
    [SerializeField] ParticleSystem[] lvCompParticles;
    bool lvCompleteParticlesPlayed = false;

    //track if this tile is being hovered over
    public bool hoverThis = false;
    //mouseFollow script
    FollowMouseScript followMouse;
    //book
    BookController bookController;

    //hint button for disabling hints after player has planted a plant
    HintButton hints;


    void Start()
    {
        plantType = -1;
        //curr_score = 0;
        turnCounter = -1;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        tileImage = this.gameObject.GetComponent<Image>();
        potImage = potImageObj.GetComponent<Image>();
        potImageRect = potImageObj.GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        bookController = GameObject.Find("BookPages").GetComponent<BookController>();
        lvCompleteParticlesPlayed = false;
        hints = GameObject.Find("Hint").GetComponent<HintButton>();
        followMouse = GameObject.Find("FollowMouse").GetComponent<FollowMouseScript>();
    }

    void Update()
    {
        if(potted_sprites == null)
        {
            LoadSpriteInfo();
        }

        displayShy();
        displayBlossom();
        if(plantType > 0)
        {
            potImageObj.SetActive(true);
            scoreDisplay.SetActive(true);
        }
        else
        {
            potImageObj.SetActive(false);
            scoreDisplay.SetActive(false);
        }

        if (falling)
        {
            fallingPot();
        }

        if (hoverThis && plantType > 0 && Input.GetMouseButtonDown(1))
        {
            bookController.OpenToPlant(plantType);
        }

        if(hoverThis && plantType > 0)
        {
            followMouse.SetPlant(plantType);
        }
    }

    void LoadSpriteInfo()
    {
        //load correct sprite data for time of day
        int timeOfDay = levelManager.GetTimeOfDay();
        if (timeOfDay == 0)
        {
            potted_sprites = GameObject.Find("DayPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        }else if(timeOfDay == 1)
        {
            potted_sprites = GameObject.Find("SunsetPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        }else if(timeOfDay == 2)
        {
            potted_sprites = GameObject.Find("NightPottedSpriteInfo").GetComponent<PottedSpriteInfo>();
        }else if(timeOfDay == 3)
        {
            Unpotted = true;
            potted_sprites = GameObject.Find("PlantsNoPotSpriteInfo").GetComponent<PottedSpriteInfo>();
        }
    }

    public void plantPlant(int plantID)
    {
        //tell hint system a player has planted
        hints.PlayerPlanted();

        //set animator to planted
        animator.SetBool("Planted", true);

        plantType = plantID;
        //TODO: initialize the current score of this tile to the base score of the plant
        //(I think this is already done in plant actions)
        //TODO: maybe do plant actions here? Currently doing them in LevelManager
        levelManager.plantAction(row, col, plantID);
        potImage.sprite = potted_sprites.get_potted_sprite(plantID);

        //moving this tile to end of child list
        //*
        int numChildren = this.transform.parent.gameObject.transform.childCount;
        this.transform.SetSiblingIndex(numChildren - 1);
        //*/

        //start pot falling animation
        Color tempColor = potImage.color;
        tempColor.a = Mathf.Min(.5f, 1.0f);
        potImage.color = tempColor;
        potImageRect.localScale = new Vector3(2f, 2f, 1f);
        falling = true;
    }

    //Increments the turn counter of this tile and returns the value of the new turn counter
    public int incCounter()
    {
        turnCounter++;
        return turnCounter;
    }

    //returns plantType
    public int getPlantType()
    {
        return plantType;
    }

    void displayShy()
    {
        shy_indicator.SetActive(false);
        if (plantType != 15) { return; }
        if (shy_toggle > 0)
        {
            shy_indicator.SetActive(true); //UNCOMMENT FOR FULL BUILD
        }
        else
        {
            potImage.sprite = potted_sprites.trigger_plant(plantType);
        }
    }

    public void toggleShy()
    {
        if(shy_toggle > 0) potAudio.playShyPlantSound();
        shy_toggle = shy_toggle * -1;
    }

    public int getShyToggle()
    {
        return shy_toggle;
    }

    void displayBlossom()
    {
        if(plantType != 17 && plantType != 18) {
            blossom_indicator.SetActive(false); 
            return; 
        }
        blossom_indicator.SetActive(true) ;

        if(plantType == 17) {
            blossom_text.SetText(Mathf.Max(0, 2 - turnCounter) + ""); 
            if(turnCounter >= 2)
            {
                blossom_indicator.SetActive(false);
                potImage.sprite = potted_sprites.bloom_plant(plantType);
                potAudio.PlayBlossom();
            }
        }
        else if(plantType == 18) { 
            blossom_text.SetText(Mathf.Max(0, 3 - turnCounter) + "");
            if (turnCounter >= 3)
            {
                blossom_indicator.SetActive(false);
                potImage.sprite = potted_sprites.bloom_plant(plantType);
                potAudio.PlayBlossom();
            }
        }
    }

    //"animation" for placing a pot down
    void fallingPot()
    {
        float time = Time.deltaTime / 2;
        if(potImage.color.a < 1f)
        {
            Color tempColor = potImage.color;
            tempColor.a = Mathf.Min(tempColor.a + (.01f + fallScale) * time, 1.0f);
            potImage.color = tempColor;
        }

        potImageRect.localScale = new Vector3(Mathf.Max(potImageRect.localScale.x - (.01f + fallScale) * time, 1f), Mathf.Max(potImageRect.localScale.y - (.01f + fallScale) * time, 1f), 1f);

        fallScale += .2f;

        if(potImage.color.a >= 1f && potImageRect.localScale.x <= 1f && potImageRect.localScale.y <= 1f)
        {
            //Debug.Log("Pot falled");
            falling = false;
            if (!Unpotted)
            {
                potAudio.playFallSound();
            }
            else
            {
                potAudio.PlayPlantUnpotted();
            }
        }
    }

    public void PointIncAnimation()
    {
        pointChangeAnimator.SetTrigger("PointIncrease");
        potAnimator.SetTrigger("PointIncrease");
    }

    public void ShiitakePointInc()
    {
        Invoke("PointIncAnimation", .5f);
    }

    public void PointDecAnimation()
    {
        pointChangeAnimator.SetTrigger("PointDecrease");
    }

    //Destroys the plant currently in this tile, removing its points and freeing the tile to be replanted
    public void DestroyPlant()
    {
        animator.SetBool("Planted", false);
        plantType = -1;
        potImage.sprite = null;
        turnCounter = 0;
    }

    public void playLvCompParticles()
    {
        if (!lvCompleteParticlesPlayed && plantType != -1)
        {
            lvCompleteParticlesPlayed = true;
            //Debug.Log("Attempting to play level complete particles for tile : Col " + col + " Row " + row);
            foreach(ParticleSystem particles in lvCompParticles)
            {
                particles.Play();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        followMouse.IncHover(plantType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        followMouse.DecHover();
    }
}
