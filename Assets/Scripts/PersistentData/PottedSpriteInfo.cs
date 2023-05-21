using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PottedSpriteInfo : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite plantedDaisy;
    [SerializeField] Sprite plantedSage;
    [SerializeField] Sprite plantedClover;
    [SerializeField] Sprite plantedBasil;
    [SerializeField] Sprite plantedSunSucc;
    [SerializeField] Sprite plantedAlfalfa;
    [SerializeField] Sprite plantedParsley;

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
            case 7:
                return plantedClover;
            case 8:
                return plantedAlfalfa;
            case 9:
                return plantedBasil;
            case 10:
                return plantedSunSucc;
            default: 
                return defaultSprite;
        }
    }
}
