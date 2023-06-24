using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lv21Cutscene : MonoBehaviour
{
    //delay b/t automatic rames
    [SerializeField] float cutsceneDelay = 3.0f;
    [SerializeField] float fadeSpeed = .1f;

    //black bg to fade in/out
    [SerializeField] Image fadeBG;

    //main camera
    [SerializeField] GameObject mainCamera;
    //Phone
    [SerializeField] Lv21Phone phone;
    //phone camera
    [SerializeField] GameObject phoneCamera;

    /*
     * state of the cutscene
     * -1 = unstarted
     * 0 = started, phone starts ringing again
     * 1 = close up of phone ringing
     * 2 = wide shot of lv 18
     * 3 = zoomed in on lv 18 to the health brochure
     * 4 = wide shot of lv 17
     * 5 = zoomed in on lv 17 to the picture frame
     * 6 = end cutscene fading to black, should move to letter scene
     */
    int cutsceneState = -1;

    //toggle for fading in/out black screen
    //0 - invisible, 1 - solid black

    private void Update()
    {
        if(cutsceneState == 0)
        {
            //phone ringing, wide shot of lv 21
        }else if(cutsceneState == 1)
        {
            //fade to black 
            Color tempColor = fadeBG.color;
            tempColor.a = Mathf.Min(tempColor.a + fadeSpeed, 1f);
            fadeBG.color = tempColor;
        }else if(cutsceneState == 2)
        {
            //fade back in 
            Color tempColor = fadeBG.color;
            tempColor.a = Mathf.Max(tempColor.a - fadeSpeed, 0f);
            fadeBG.color = tempColor;
        }else if (cutsceneState == 3)
        {
            //fade to black 
            Color tempColor = fadeBG.color;
            tempColor.a = Mathf.Min(tempColor.a + fadeSpeed, 1f);
            fadeBG.color = tempColor;
        }
    }

    public void StartCutscene()
    {
        //Destroy lv complete screen
        Destroy(GameObject.FindWithTag("debug_NextLv"));
        //fade music out
        StartCoroutine(MusicFade(2.0f, 0.0f));
        //ring phone if not ringing
        phone.PhoneCall();

        //set state
        cutsceneState = 0;
        Invoke("State1", cutsceneDelay);
    }

    void State1()
    {
        cutsceneState++;
        Invoke("State2", cutsceneDelay);
    }

    void State2()
    {
        mainCamera.SetActive(false);
        phoneCamera.SetActive(true);
        cutsceneState++;
        Invoke("State3", cutsceneDelay);
    }

    void State3()
    {
        cutsceneState++;
        Invoke("State4", cutsceneDelay);
    }

    void State4()
    {
        SceneManager.LoadScene("StartAct3Cutscene");
    }

    void ZoomPhone()
    {
        StartCoroutine(FadeIn());
    }

    void FadeToNextCutscene()
    {
        StartCoroutine(FadeToNext());
    }

    //coroutines for fading
    IEnumerator FadeIn()
    {
        Color c = fadeBG.color;
        for(float alpha = 0.0f;  alpha <= 1.0f; alpha += .1f)
        {
            c.a = alpha;
            fadeBG.color = c;
            yield return null;
        }

        StopCoroutine(FadeIn());
        //when black background is up, switch cameras
        mainCamera.SetActive(false);
        phoneCamera.SetActive(true);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color c = fadeBG.color;
        for (float alpha = 1.0f; alpha >= 0.0f; alpha -= .1f)
        {
            c.a = alpha;
            fadeBG.color = c;
            yield return null;
        }

        StopCoroutine(FadeOut());
        Invoke("FadeToNextCutscene", cutsceneDelay);
    }

    IEnumerator FadeToNext()
    {
        Color c = fadeBG.color;
        for (float alpha = 0.0f; alpha <= 1.0f; alpha += .1f)
        {
            c.a = alpha;
            fadeBG.color = c;
            yield return null;
        }

        SceneManager.LoadScene("StartAct3Cutscene");
    }

    IEnumerator MusicFade(float duration, float targetVolume)
    {
        AudioSource audioSource = GameObject.Find("Act2Levels").GetComponent<AudioSource>();
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
