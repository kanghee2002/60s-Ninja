using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource audioSourceBGM;
    public AudioSource audioSourceEffect;

    public AudioClip BGMMainMenu;
    public AudioClip BGMInGame;

    public bool isFadeRunning;
    public bool fadeInFlag;
    public bool fadeOutFlag;

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


        audioSourceBGM = transform.GetChild(0).GetComponent<AudioSource>();
        audioSourceEffect = transform.GetChild(1).GetComponent<AudioSource>();
    }

    void Start()
    {
        Initialization();
    }

    private void Update()
    {
        FadeInOut();
    }
    
    void Initialization()
    {
        audioSourceBGM.clip = BGMMainMenu;
        audioSourceBGM.Play();
        fadeInFlag = true;
        fadeOutFlag = false;
    }

    void FadeInOut()
    {
        if (fadeInFlag)
        {
            audioSourceBGM.clip = BGMMainMenu;
            audioSourceBGM.volume += 0.01f;

            if (audioSourceBGM.volume > 0.99f)
            {
                fadeInFlag = false;
            }
        }
        
        if (fadeOutFlag)
        {
            audioSourceBGM.volume -= 0.01f;

            if (audioSourceBGM.volume < 0.01f)
            {
                fadeInFlag = false;
            }
        }

    }

    public void BGMInGameStart()
    {
        audioSourceBGM.clip = BGMInGame;
        audioSourceBGM.volume = 1;
        fadeInFlag = false;
        fadeOutFlag = false;
        audioSourceBGM.Play();
    }

    public void EffectPlay(AudioClip clip)
    {
        audioSourceEffect.PlayOneShot(clip);
    }

}
