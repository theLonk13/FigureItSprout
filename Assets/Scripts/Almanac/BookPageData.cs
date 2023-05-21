using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPageData : MonoBehaviour
{
    [SerializeField] int pgNumber; //Page number of this page in the book
    [SerializeField] int plantID;  //ID of the plant this page displays

    RectTransform pgTransform;

    void Awake()
    {
        int csPgNumber = pgNumber - 1;
        pgTransform = GetComponent<RectTransform>();
        pgTransform.localPosition = new Vector3(
            -11 + 50 * (csPgNumber / 2) + 22 * (csPgNumber % 2),
            1,
            0
        );
    }

    public int getPlantID()
    {
        return plantID;
    }

    public int getPgNum()
    {
        return pgNumber;
    }
}
