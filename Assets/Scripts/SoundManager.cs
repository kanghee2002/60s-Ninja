using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BGM,
    Effect,
}

public enum SceneType
{
    MainMenu,
    InGame,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    private AudioSource audioSourceBGM;

    [SerializeField]
    private AudioSource audioSourceEffect;

    [SerializeField]
    private AudioClip BGMMainMenu;

    [SerializeField]
    private AudioClip BGMInGame;

    public bool isBGMMute => audioSourceBGM.mute;

    public bool isEffectMute => audioSourceEffect.mute;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartBGM(SceneType sceneType)
    {
        audioSourceBGM.clip = sceneType switch
        {
            SceneType.InGame => BGMInGame,
            SceneType.MainMenu => BGMMainMenu,
            _ => BGMMainMenu,
        };

        audioSourceBGM.volume = 1;
        audioSourceBGM.Play();
    }

    public void SetAudioVolume(SoundType soundType, float volume)
    {
        var audioSource = soundType switch
        {
            SoundType.BGM => audioSourceBGM,
            SoundType.Effect => audioSourceEffect,
            _ => audioSourceBGM,
        };

        audioSource.volume = volume; 
    }

    public void SetMute(SoundType soundType, bool on)
    {
        var audioSource = soundType switch
        {
            SoundType.BGM => audioSourceBGM,
            SoundType.Effect => audioSourceEffect,
            _ => audioSourceBGM,
        };
        audioSource.mute = on;
    }

    public void EffectPlay(AudioClip clip)
    {
        audioSourceEffect.PlayOneShot(clip);
    }

}
