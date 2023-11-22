using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSettings : MonoBehaviour
{
    [SerializeField] Animator settingsAnim;
    bool showSettings = false;

    LevelData lvData;

    void Start()
    {
        settingsAnim.SetBool("ShowSettings", showSettings);
        lvData = GameObject.Find("LevelData").GetComponent<LevelData>();
    }

    public void ToggleSettings()
    {
        showSettings = !showSettings;
        settingsAnim.SetBool("ShowSettings", showSettings);
    }

    public void UnlockLevels(int actNum)
    {
        lvData.UnlockAct(actNum);
    }
}
