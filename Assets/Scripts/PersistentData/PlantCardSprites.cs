using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCardSprites : MonoBehaviour
{
    //Sprites for initial potted plants
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite daisyCard;
    [SerializeField] Sprite sageCard;
    [SerializeField] Sprite cloverCard;
    [SerializeField] Sprite basilCard;
    [SerializeField] Sprite sunSuccCard;
    [SerializeField] Sprite alfalfaCard;
    [SerializeField] Sprite parsleyCard;
    [SerializeField] Sprite crabgrassCard;
    [SerializeField] Sprite lemongrassCard;

    [SerializeField] Sprite orchid10Card;
    [SerializeField] Sprite orchid9Card;
    [SerializeField] Sprite orchid7Card;
    [SerializeField] Sprite orchid4Card;
    [SerializeField] Sprite orchid2Card;

    [SerializeField] Sprite shiitakeCard;
    [SerializeField] Sprite shyCard;
    [SerializeField] Sprite vipergrassCard;
    [SerializeField] Sprite sunflowerCard;
    [SerializeField] Sprite carnationCard;
    [SerializeField] Sprite thymeCard;

    public Sprite GetPlantCardSprite(int plantID)
    {
        switch (plantID)
        {
            case 1:
                return daisyCard;
            case 2:
                return sageCard;
            case 3:
                return parsleyCard;
            case 5:
                return lemongrassCard;
            case 6:
                return thymeCard;
            case 7:
                return cloverCard;
            case 8:
                return alfalfaCard;
            case 9:
                return basilCard;
            case 10:
                return sunSuccCard;
            case 11:
                return vipergrassCard;
            case 12:
                return crabgrassCard;
            case 13:
                return shiitakeCard;
            case 15:
                return shyCard;
            case 16:
                switch (GetLevelNum())
                {
                    case 17:
                        return orchid10Card;
                    case 18:
                        return orchid9Card;
                    case 19:
                        return orchid7Card;
                    case 20:
                        return orchid4Card;
                    case 21:
                        return orchid2Card;
                    default:
                        return defaultSprite;
                }
            case 17:
                return sunflowerCard;
            case 18:
                return carnationCard;
            default:
                return defaultSprite;
        }
    }

    int GetLevelNum()
    {
        LevelManager lvMan = GameObject.Find("LevelUICanvas").GetComponent<LevelManager>();
        if (lvMan != null)
        {
            return lvMan.GetLevelNum();
        }
        return -1;
    }
}
