using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectIcon : MonoBehaviour
{
    [SerializeField] int lvNum;
    [SerializeField] string LevelSceneName;

    //Level icon button and text
    [SerializeField] TextMeshProUGUI levelText;

    //Bonus Points indicator/flag
    [SerializeField] bool BonusAvailable = false;
    [SerializeField] GameObject BonusNotAcquired;
    [SerializeField] GameObject BonusAcquired;

    //audio for button
    AudioSource buttonAudio;

    // Start is called before the first frame update
    void Start()
    {
        levelText.SetText("Level " + lvNum);
        buttonAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!BonusAvailable)
        {
            BonusNotAcquired.SetActive(false);
            BonusAcquired.SetActive(false);
        }
    }

    public void JumpToLevel()
    {
        buttonAudio.Play();
        SceneManager.LoadScene(LevelSceneName);
    }

    public int getLvNum()
    {
        return lvNum;
    }

    public void ShowBonusNotAcquired()
    {
        if (BonusAvailable)
        {
            BonusNotAcquired.SetActive(true);
            BonusAcquired.SetActive(false);
        }
    }

    public void ShowBonusAcquired()
    {
        if (BonusAvailable)
        {
            BonusAcquired.SetActive(true);
            BonusNotAcquired.SetActive(false);
        }
    }
}
