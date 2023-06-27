using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JenniferAnnPlay : MonoBehaviour
{
    [SerializeField] CutsceneManager cutscene;

    void Awake()
    {
        Invoke("playJennAnn", .5f);
    }

    void playJennAnn()
    {
        cutscene.AdvanceFrame();
    }
}
