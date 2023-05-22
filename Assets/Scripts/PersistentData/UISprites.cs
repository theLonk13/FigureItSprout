using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISprites : MonoBehaviour
{
    [SerializeField] Sprite startIdle;
    [SerializeField] Sprite startHighlight;

    public Sprite getStartIdle() { return startIdle; }
    public Sprite getStartHighlight() { return startHighlight; }
}
