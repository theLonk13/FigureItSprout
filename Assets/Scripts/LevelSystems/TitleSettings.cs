using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSettings : MonoBehaviour
{
    [SerializeField] Animator settingsAnim;
    bool showSettings = false;

    LevelData lvData;
    int[] actSkips;

    bool[] confirmScreens;

    //Sprites for unlock buttons
    [SerializeField] Sprite act1locked;
    [SerializeField] Sprite act1unlocked;
    [SerializeField] Sprite act2locked;
    [SerializeField] Sprite act2unlocked;
    [SerializeField] Sprite act3locked;
    [SerializeField] Sprite act3unlocked;

    //actual button images
    [SerializeField] Image act1Button;
    [SerializeField] Image act2Button;
    [SerializeField] Image act3Button;

    //confirmation screens for unlocking
    [SerializeField] GameObject act1UnlockConfirm;
    [SerializeField] GameObject act2UnlockConfirm;
    [SerializeField] GameObject act3UnlockConfirm;

    void Start()
    {
        settingsAnim.SetBool("ShowSettings", showSettings);
        lvData = GameObject.Find("LevelData").GetComponent<LevelData>();
        confirmScreens = new bool[3];
    }

    void Update()
    {
        if (lvData != null) { actSkips = lvData.GetActSkips(); }
        UpdateUnlockButtonSprites();
        UpdateUnlockConfirmScreens();

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

    void UpdateUnlockButtonSprites()
    {
        if (actSkips != null)
        {
            if (actSkips[0] == 1 && act1Button != null)
            {
                act1Button.sprite = act1unlocked;
            }
            else
            {
                act1Button.sprite = act1locked;
            }

            if (actSkips[1] == 1 && act2Button != null)
            {
                act2Button.sprite = act2unlocked;
            }
            else
            {
                act2Button.sprite = act2locked;
            }

            if (actSkips[2] == 1 && act3Button != null)
            {
                act3Button.sprite = act3unlocked;
            }
            else
            {
                act3Button.sprite = act3locked;
            }
        }
    }

    public void ToggleUnlockConfirm(int actNum)
    {
        //Debug.Log("Trying to unlock " + actNum);
        if(actNum <= confirmScreens.Length)
        {
            confirmScreens[actNum - 1] = !confirmScreens[actNum - 1];
        }
    }

    void UpdateUnlockConfirmScreens()
    {
        act1UnlockConfirm.SetActive(confirmScreens[0]);
        act2UnlockConfirm.SetActive(confirmScreens[1]);
        act3UnlockConfirm.SetActive(confirmScreens[2]);
    }
}
