using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonClickSound;

    [SerializeField]
    private ClickUI[] InGameButtons;

    [SerializeField]
    private ClickUI[] MainMenuButtons;

    [SerializeField]
    private GameObject menuUI;

    [SerializeField]
    private GameObject gameOverUI;

    public void InitMainMenu()
    {
        foreach (var button in MainMenuButtons)
        {
            button.AddListenerOnly(() =>
            SoundManager.instance.EffectPlay(buttonClickSound));
        }

        //GameStart, HowToPlay, Setting, Quit, Back, BGM, Effect
        MainMenuButtons[0].AddListener(() => GameStartButtonAction());
        MainMenuButtons[1].AddListener(() => HowToPlayButtonAction());
        MainMenuButtons[2].AddListener(() => SettingButtonAction());
        MainMenuButtons[3].AddListener(() => QuitButtonAction());
        MainMenuButtons[4].AddListener(() => BackButtonAction());
        MainMenuButtons[5].AddListener(() => BGMButtonAction());
        MainMenuButtons[6].AddListener(() => EffectButtonAction());
    }

    public void InitInGame()
    {
        foreach(var button in InGameButtons)
        {
            button.AddListenerOnly(() =>
            SoundManager.instance.EffectPlay(buttonClickSound));
        }

        //Setting, Retry, Main, Continue, BGM, Effect, Main
        InGameButtons[0].AddListener(() => SettingButtonAction());
        InGameButtons[1].AddListener(() => RetryButtonAction());
        InGameButtons[2].AddListener(() => MainButtonAction());
        InGameButtons[3].AddListener(() => ContinueButtonAction());
        InGameButtons[4].AddListener(() => BGMButtonAction());
        InGameButtons[5].AddListener(() => EffectButtonAction());
        InGameButtons[6].AddListener(() => MainButtonAction());
    }

    private void BGMButtonAction()
    {
        SoundManager.instance.SetMute(SoundType.BGM, 
            !SoundManager.instance.isBGMMute);
    }

    private void EffectButtonAction()
    {
        SoundManager.instance.SetMute(SoundType.Effect, 
            !SoundManager.instance.isEffectMute);
    }

    private void SettingButtonAction()
    {
        if (InGameManager.instance != null)
        {
            InGameManager.instance.isGaming = false;
        }
        Time.timeScale = 0f;
        menuUI.SetActive(true);
    }

    private void ContinueButtonAction()
    {
        InGameManager.instance.isGaming = true;
        Time.timeScale = 1f;
        menuUI.SetActive(false);
    }

    private void GameStartButtonAction()
    {
        SceneManager.LoadScene("InGame");

        Time.timeScale = 1f;
    }

    private void HowToPlayButtonAction()
    {
        SceneManager.LoadScene("HowToPlay");

        Time.timeScale = 1f;

        //InGameManager.instance.isInitialized = false;
        //InGameManager.instance.generateBar.SetActive(false);
    }

    private void MainButtonAction()
    {
        SceneManager.LoadScene("MainMenu");

        SoundManager.instance.StartBGM(SceneType.MainMenu);
        SoundManager.instance.SetAudioVolume(SoundType.BGM, 0);

        Time.timeScale = 1f;
    }

    private void RetryButtonAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        InGameManager.instance.InitGame();
    }

    private void BackButtonAction()
    {
        menuUI.SetActive(false);
    }

    private void QuitButtonAction()
    {
        Application.Quit();
    }
}
