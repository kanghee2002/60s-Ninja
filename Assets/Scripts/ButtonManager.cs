using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private ClickUI[] MainMenuButtons;

    [SerializeField]
    private ClickUI[] InGameButtons;

    [SerializeField]
    private ClickUI[] TutorialButtons;

    [SerializeField]
    private GameObject menuUI;

    [SerializeField]
    private GameObject gameOverUI;

    private bool isSettingButtonActive = false;

    public void InitMainMenu()
    {
        AddButtonClickSound(MainMenuButtons);

        //GameStart, HowToPlay, Setting, Quit, Back, BGM, Effect
        MainMenuButtons[0].AddListener(() => GameStartButtonAction());
        MainMenuButtons[1].AddListener(() => HowToPlayButtonAction());
        MainMenuButtons[2].AddListener(() => SettingButtonAction());
        MainMenuButtons[3].AddListener(() => QuitButtonAction());
        MainMenuButtons[4].AddListener(() => BackButtonAction());
        MainMenuButtons[5].AddListener(() => BGMButtonAction());
        MainMenuButtons[6].AddListener(() => EffectButtonAction());
    }

    public void InitTutorial()
    {
        AddButtonClickSound(TutorialButtons);

        //Setting, Continue, BGM, Effect, Main
        TutorialButtons[0].AddListener(() => SettingButtonAction());
        TutorialButtons[1].AddListener(() => ContinueButtonAction());
        TutorialButtons[2].AddListener(() => BGMButtonAction());
        TutorialButtons[3].AddListener(() => EffectButtonAction());
        TutorialButtons[4].AddListener(() => MainButtonAction());
        TutorialButtons[5].AddListener(() => RetryButtonAction());
        TutorialButtons[6].AddListener(() => MainButtonAction());
    }

    public void InitInGame()
    {
        AddButtonClickSound(InGameButtons);

        //Setting, Retry, Main, Continue, BGM, Effect, Main
        InGameButtons[0].AddListener(() => SettingButtonAction());
        InGameButtons[1].AddListener(() => RetryButtonAction());
        InGameButtons[2].AddListener(() => MainButtonAction());
        InGameButtons[3].AddListener(() => ContinueButtonAction());
        InGameButtons[4].AddListener(() => BGMButtonAction());
        InGameButtons[5].AddListener(() => EffectButtonAction());
        InGameButtons[6].AddListener(() => MainButtonAction());
    }

    private void AddButtonClickSound(ClickUI[] clickUIArr)
    {
        foreach (var button in clickUIArr)
        {
            button.AddListenerOnly(() =>
            SoundManager.instance.PlaySFX("ClickButton"));
        }
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

        if (isSettingButtonActive)
        {
            menuUI.SetActive(false);
        }
        else
        {
            menuUI.SetActive(true);
        }
        isSettingButtonActive = !isSettingButtonActive;
    }

    private void ContinueButtonAction()
    {
        InGameManager.instance.isGaming = true;

        Time.timeScale = 1f;

        isSettingButtonActive = false;
        menuUI.SetActive(false);
    }

    private void GameStartButtonAction()
    {
        SceneManager.LoadScene("InGame");

        SoundManager.instance.PlayBGM(SceneType.NULL);

        Time.timeScale = 1f;
    }

    private void HowToPlayButtonAction()
    {
        SceneManager.LoadScene("Tutorial");

        SoundManager.instance.PlayBGM(SceneType.NULL);

        InGameManager.instance.isTutorialExplaining = true;

        Time.timeScale = 1f;
    }

    private void MainButtonAction()
    {
        SceneManager.LoadScene("MainMenu");

        SoundManager.instance.PlayBGM(SceneType.MainMenu);

        InGameManager.instance.isTutorialExplaining = false;

        Time.timeScale = 1f;
    }

    private void RetryButtonAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        InGameManager.instance.InitGame();
    }

    private void BackButtonAction()
    {
        isSettingButtonActive = false;
        menuUI.SetActive(false);
    }

    private void QuitButtonAction()
    {
        Application.Quit();
    }
}
