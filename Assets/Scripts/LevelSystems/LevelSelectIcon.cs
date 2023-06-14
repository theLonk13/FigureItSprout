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
}
