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
    NULL,
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

    private Dictionary<string, AudioClip> audioClipDic = new();

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

        LoadAudioClip();
    }

    public void PlayBGM(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.InGame:
                audioSourceBGM.clip = BGMInGame;
                break;
            case SceneType.MainMenu:
                audioSourceBGM.clip = BGMMainMenu;
                break;
            case SceneType.NULL:
                audioSourceBGM.clip = null;
                break;
        }
        audioSourceBGM.Play();
        audioSourceBGM.volume = 1;
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

    public void PlaySFX(string clip)
    {
        audioSourceEffect.PlayOneShot(audioClipDic[clip]);
    }

    private void LoadAudioClip()
    {
        var audioClips = ResourceDictionary.GetAll<AudioClip>("Sounds");

        foreach(var audioClip in audioClips)
        {
            audioClipDic.Add(audioClip.name, audioClip);
        }
    }
}
