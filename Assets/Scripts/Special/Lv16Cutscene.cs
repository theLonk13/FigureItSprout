using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv16Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MusicFade(5.0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
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
