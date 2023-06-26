using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusIcon : MonoBehaviour
{
    //number of bonus stars needed for this reward
    [SerializeField] int starsRequired = 0;

    //Image that is the bonus reward but full size
    [SerializeField] GameObject bonusImageBG;
    [SerializeField] GameObject bonusImageObj;
    Image bonusImage;
    bool showBonus = false;

    [SerializeField] TextMeshProUGUI starsRequiredText;

    // Start is called before the first frame update
    void Start()
    {
        bonusImage = bonusImageObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showBonus)
        {
            //moving this frame to end of child list
            //*
            int numChildren = this.transform.parent.gameObject.transform.childCount;
            this.transform.SetSiblingIndex(numChildren - 1);

            bonusImageBG.SetActive(true);
            bonusImageObj.SetActive(true);
            Color tempColor = bonusImage.color;
            tempColor.a = Mathf.Min(tempColor.a + .02f, 1f);
            bonusImage.color = tempColor;
        }
        else
        {
            bonusImageBG.SetActive(false);
            bonusImageObj.SetActive(false);
            Color tempColor = bonusImage.color;
            tempColor.a = Mathf.Max(tempColor.a - .02f, 0f);
            bonusImage.color = tempColor;
        }

        starsRequiredText.SetText("" + starsRequired);
    }

    public void ToggleBonus()
    {
        showBonus = !showBonus;
    }

    public int GetStarsRequired()
    {
        return starsRequired;
    }
}
