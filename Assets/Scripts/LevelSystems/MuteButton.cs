using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    Button button;
    GameObject musicObj;
    MusicPlayer musicPlayer = null;

    //Sprites for the mute and unmute
    [SerializeField] Sprite muteSymbol;
    [SerializeField] Sprite unmuteSymbol;
    [SerializeField] Image muteButtonImage;

    bool muted = false;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        musicObj = GameObject.Find("MusicPlayer");
        if(musicObj != null)
        {
            musicPlayer = musicObj.GetComponent<MusicPlayer>();
        }

        if(musicPlayer != null && button != null)
        {
            button.onClick.AddListener(musicPlayer.ToggleMuteMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(musicPlayer != null)
        {
            muted = musicPlayer.GetIsMuted();
        }
        if(muteButtonImage != null)
        {
            if (muted)
            {
                muteButtonImage.sprite = unmuteSymbol;
            }
            else
            {
                muteButtonImage.sprite = muteSymbol;
            }
        }
    }
}
