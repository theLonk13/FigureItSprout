using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lv21Cutscene : MonoBehaviour
{
    //delay b/t automatic rames
    [SerializeField] float cutsceneDelay = 3.0f;
    [SerializeField] float fadeSpeed = 40f;

    //black bg to fade in/out
    [SerializeField] Image fadeBG;

    //Background
    [SerializeField] RectTransform levelBackground;
    //Phone
    [SerializeField] Lv21Phone phone;

    //next part of the cutscene
    [SerializeField] GameObject cutscenePart2;

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
            tempColor.a = Mathf.Min(tempColor.a + fadeSpeed * Time.deltaTime, 1f);
            fadeBG.color = tempColor;
        }else if(cutsceneState == 2)
        {
            //fade back in 
            Color tempColor = fadeBG.color;
            tempColor.a = Mathf.Max(tempColor.a - fadeSpeed * Time.deltaTime, 0f);
            fadeBG.color = tempColor;
        }else if (cutsceneState == 3)
        {
            //fade to black 
            Color tempColor = fadeBG.color;
            tempColor.a = Mathf.Min(tempColor.a + fadeSpeed * Time.deltaTime, 1f);
            fadeBG.color = tempColor;
        }
    }

    public void StartCutscene()
    {
        Debug.Log("Lv21Cutscene Starting Cutscene");

        //Destroy lv complete screen
        Destroy(GameObject.FindWithTag("debug_NextLv"));
        //fade music out
        StartCoroutine(MusicFade(2.0f, 0.0f));
        //ring phone if not ringing
        phone.PhoneCall(true);

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
        levelBackground.localPosition = new Vector3(-400f, -160f, -5f);
        levelBackground.localScale = new Vector3(2f, 2f, 2f);
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
        cutscenePart2.SetActive(true);
        Invoke("AutoPlayCutscene", .2f);
    }

    void AutoPlayCutscene()
    {
        CutsceneManager cutsceneMan = cutscenePart2.GetComponent<CutsceneManager>();
        if (cutsceneMan != null) { cutsceneMan.AdvanceFrame(); }
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

        audioSource.Stop();
        SceneData sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        sceneData.ChangeMusicState(7);
        audioSource.volume = 1.0f;
        yield break;
    }
}
