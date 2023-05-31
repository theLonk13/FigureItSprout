using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PottedSpriteInfo : MonoBehaviour
{
    //Sprites for initial potted plants
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite plantedDaisy;
    [SerializeField] Sprite plantedSage;
    [SerializeField] Sprite plantedClover;
    [SerializeField] Sprite plantedBasil;
    [SerializeField] Sprite plantedSunSucc;
    [SerializeField] Sprite plantedAlfalfa;
    [SerializeField] Sprite plantedParsley;
    [SerializeField] Sprite plantedCrabgrass;
    [SerializeField] Sprite plantedLemongrass;
    [SerializeField] Sprite plantedOrchid;
    [SerializeField] Sprite plantedShiitake;
    [SerializeField] Sprite plantedShy;
    [SerializeField] Sprite plantedVipergrass;
    [SerializeField] Sprite plantedSunflower;
    [SerializeField] Sprite plantedCarnation;

    //Sprites for triggered plants
    [SerializeField] Sprite triggeredShy;

    //Sprites for bloomed plants
    [SerializeField] Sprite bloomedSunflower;
    [SerializeField] Sprite bloomedCarnation;

    public Sprite get_potted_sprite(int plantID)
    {
        switch (plantID)
        {
            case 1:
                return plantedDaisy;
            case 2:
                return plantedSage;
            case 3:
                return plantedParsley;
            case 5:
                return plantedLemongrass;
            case 7:
                return plantedClover;
            case 8:
                return plantedAlfalfa;
            case 9:
                return plantedBasil;
            case 10:
                return plantedSunSucc;
            case 11:
                return plantedVipergrass;
            case 12:
                return plantedCrabgrass;
            case 13:
                return plantedShiitake;
            case 15:
                return plantedShy;
            case 16:
                return plantedOrchid;
            case 17:
                return plantedSunflower;
            case 18:
                return plantedCarnation;
            default: 
                return defaultSprite;
        }
    }

    public Sprite trigger_plant(int plantID)
    {
        switch (plantID)
        {
            case 15:
                return triggeredShy;
            default:
                return defaultSprite;
        }
    }

    public Sprite bloom_plant(int plantID)
    {
        switch(plantID)
        {
            case 17:
                return bloomedSunflower;
            case 18:
                return bloomedCarnation;
            default:
                return defaultSprite;
        }
    }
}
