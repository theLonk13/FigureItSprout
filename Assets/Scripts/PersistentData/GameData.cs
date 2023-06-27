using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Texture2D cursorSprite;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.ForceSoftware);    
    }

}
