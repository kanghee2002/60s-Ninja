using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public AudioClip ButtonClickSound;

    public GameObject menuUI;

    AudioSource audioSourceBGM;
    AudioSource audioSourceEffect;

    public Slider BGMSlider;
    public Slider EffectSlider;

    public GameObject fadeObj;
    public GameObject gameOverUI;

    void Start()
    {
        audioSourceBGM = SoundManager.instance.audioSourceBGM;
        audioSourceEffect = SoundManager.instance.audioSourceEffect;

        if (gameOverUI != null)
        {
            GameManager.instance.gameOverUI = gameOverUI;
        }
    }


    public void BGMBtn()
    {
        if (audioSourceBGM.mute == false)
        {
            audioSourceBGM.mute = true;
        }
        else
        {
            audioSourceBGM.mute = false;
        }
        SoundManager.instance.EffectPlay(ButtonClickSound);
    }
    public void EffectBtn()
    {
        if (audioSourceEffect.mute == false)
        {
            audioSourceEffect.mute = true;
        }
        else
        {
            audioSourceEffect.mute = false;
        }
        SoundManager.instance.EffectPlay(ButtonClickSound);
    }

    public void SettingBtn()
    {
        SoundManager.instance.EffectPlay(ButtonClickSound);
        GameManager.instance.isGaming = false;
        Time.timeScale = 0f;
        menuUI.SetActive(true);
    }

    public void ContinueBtn()
    {
        SoundManager.instance.EffectPlay(ButtonClickSound);

        GameManager.instance.isGaming = true;
        Time.timeScale = 1f;
        menuUI.SetActive(false);
    }

    public void GameStartBtn()
    {
        Time.timeScale = 1f;

        fadeObj.SetActive(true);
        fadeObj.GetComponent<FadeInOut>().StartCoroutine(fadeObj.GetComponent<FadeInOut>().FadeOut("InGame"));

        SoundManager.instance.fadeOutFlag = true;
        SoundManager.instance.fadeInFlag = false;
        SoundManager.instance.EffectPlay(ButtonClickSound);

        GameManager.instance.isInitialized = false;
    }

    public void HowToPlayBtn()
    {
        Time.timeScale = 1f;

        fadeObj.SetActive(true);
        fadeObj.GetComponent<FadeInOut>().StartCoroutine(fadeObj.GetComponent<FadeInOut>().FadeOut("Tutorial"));

        SoundManager.instance.EffectPlay(ButtonClickSound);

        GameManager.instance.isInitialized = false;
        GameManager.instance.generateBar.SetActive(false);
    }

    public void MainBtn()
    {
        fadeObj.SetActive(true);
        fadeObj.GetComponent<FadeInOut>().StartCoroutine(fadeObj.GetComponent<FadeInOut>().FadeOut("MainMenu"));

        SoundManager.instance.EffectPlay(ButtonClickSound);
        audioSourceBGM.clip = SoundManager.instance.BGMMainMenu;
        audioSourceBGM.Play();
        audioSourceBGM.volume = 0;
        SoundManager.instance.fadeInFlag = true;
        SoundManager.instance.fadeOutFlag = false;

        GameManager.instance.isGaming = true;
        GameManager.instance.generateBar.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RetryBtn()
    {
        SoundManager.instance.EffectPlay(ButtonClickSound);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GameManager.instance.isInitialized = false;

    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
