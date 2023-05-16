using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileInfoDisplay : MonoBehaviour
{
    [SerializeField] LevelTile tiledata;
    [SerializeField] TextMeshPro scoreDisplay;

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.SetText(tiledata.curr_score + "");
    }
}
