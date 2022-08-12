using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    ButtonManager buttonManager;

    [SerializeField]
    GameObject fade;

    private void Start()
    {
        buttonManager.InitMainMenu();
        StartCoroutine(SetFade(InGameManager.FadeType.FadeIn));
        SoundManager.instance.StartBGM(SoundManager.SceneType.MainMenu);
    }

    private IEnumerator SetFade(InGameManager.FadeType fadeType)
    {
        fade.SetActive(true);

        var image = fade.GetComponent<Image>();

        image.color = fadeType == InGameManager.FadeType.FadeIn ?
            new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);

        var color = image.color;

        float colorChage;

        (image.color, colorChage) = fadeType switch
        {
            InGameManager.FadeType.FadeIn => (new Color(0, 0, 0, 1), -0.05f),
            InGameManager.FadeType.FadeOut => (new Color(0, 0, 0, 0), 0.05f),
            _ => (new Color(0, 0, 0, 1), -0.1f)                //Error
        };

        while (0 <= color.a && color.a <= 1)
        {
            color.a += colorChage;
            image.color = color;

            yield return null;
        }

        fade.SetActive(false);

        yield break;
    }
}
