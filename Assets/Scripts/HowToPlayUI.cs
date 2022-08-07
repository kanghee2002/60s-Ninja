using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayUI : MonoBehaviour
{
    public AudioClip buttonClickSound;

    public Image moonImage;
    public RectTransform shadowRectTransform;
    float time;

    public GameObject canvas1;
    public GameObject canvas2;

    public GameObject fadeObj;

    void Start()
    {
        time = 60f;
    }


    void Update()
    {
        time -= 0.4f;
        shadowRectTransform.anchoredPosition = new Vector3((60 - time) * 5 / 6, -((60 - time) * 5 / 6), 0);        //0, 0 -> 50, -50
        if (time < 0f)
        {
            time = 60f;
        }
        moonImage.color = new Color(1, time / 20f, time / 20f);      // After 20s  1 -> 0
    }

    public void NextBtn()
    {
        SoundManager.instance.EffectPlay(buttonClickSound);
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }
    public void BackBtn()
    {
        SoundManager.instance.EffectPlay(buttonClickSound);
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }
    public void MainBtn()
    {
        SoundManager.instance.EffectPlay(buttonClickSound);
        fadeObj.SetActive(true);
        fadeObj.GetComponent<FadeInOut>().StartCoroutine(fadeObj.GetComponent<FadeInOut>().FadeOut("MainMenu"));
    }
}