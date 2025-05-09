﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum FadeType { FadeIn, FadeOut };

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance = null;

    //[Debug]//
    public bool isPlayerImmortal;

    public bool isTutorialExplaining { get; set; }

    [Header("GameObject")]
    [SerializeField]
    private GameObject playerObj;

    [SerializeField]
    private GameObject wallObj;

    [SerializeField]
    private GameObject generateBar;

    [SerializeField]
    private GameObject fade;


    public GameObject player { get; private set; }

    public int playerScore { get; private set; }

    public float playerPlayTime { get; private set; }

    public int playerScoreLevel { get; private set; }    //1, 2, 3, 4, 5

    public float playerTime { get; private set; }

    public bool isGameStart { get; private set; }

    public bool isGaming { get; set; }


    [Header("UI")]

    [SerializeField]
    private ButtonManager buttonManager;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject addScoreText;

    [SerializeField]
    private GameObject addTimeText;


    [Header("Text")]
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI resultTimerText;

    public TextMeshProUGUI resultScoreText;


    [Header("Format")]
    [SerializeField]
    private int[] scoreLevelLimits;

    [SerializeField]
    private int[] levelProbabilitys; 

    [SerializeField]
    private Transform objectsParent;

    [SerializeField]
    private float wallHeight;

    private float curFormatPosY;

    private float curWallPosY;

    private List<List<Format>> formatsList = new();


    [Header("Test")]
    [SerializeField]
    private float timeMultiplier;

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
    }

    private void Start()
    {
        Init();
        InitGame();
    }

    private void Init()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            buttonManager.InitTutorial();
            return;
        }

        generateBar.SetActive(true);
        MakeFormatList();
        buttonManager.InitInGame();
    }

    private void Update()
    {
        CountPlayerTime();
        SetText();

    }

    [ContextMenu("_AddPlayerScoreLevel")]
    private void _AddPlayerScoreLevel()
    {
        if (playerScoreLevel >= 5)
        {
            Debug.Log(playerScoreLevel);
            return;
        }
        playerScoreLevel++;
        Debug.Log(playerScoreLevel);
    }

    [ContextMenu("_NoDie")]
    private void _NoDie()
    {
        isPlayerImmortal = true;
        playerTime = 6000f;
    }

    public void StartGame()
    {
        isGameStart = true;
        isGaming = true;
        SoundManager.instance.PlayBGM(SceneType.InGame);
    }

    public void InitGame()
    {
        if (player == null)
        {
            player = Instantiate(playerObj, Vector3.zero, transform.rotation);
        }
        else
        {
            player.SetActive(true);
            player.transform.position = Vector3.zero;
        }

        player.GetComponent<Player>().Init();

        StartCoroutine(SetFade(FadeType.FadeIn));

        playerScore = 0;
        playerPlayTime = 0;
        playerScoreLevel = 1;
        playerTime = 60f;
        isGameStart = false;
        isGaming = false;
        curFormatPosY = 30f;
        curWallPosY = 40f;
    }

    private void CountPlayerTime()
    {
        if (isGameStart && isGaming)
        {
            playerPlayTime += Time.deltaTime;
            playerTime -= Time.deltaTime;
            if (playerTime < 0)
            {
                GameOver();
            }
        }
    }

    private void SetText()
    {
        timerText.text = string.Format("{0:N1}", playerTime);
        scoreText.text = playerScore.ToString();
    }

    public void AddPlayerTime(float score)
    {
        float addTime = timeMultiplier * score;

        if (addTime < 0) addTime = 0;

        playerTime += addTime;

        if (playerTime > 60f)
        {
            playerTime = 60f;
        }

        SetAddTimeText(addTime);
    }

    public void AddPlayerScore(int score)
    {
        playerScore += score;

        SetAddScoreText(score);

        //Set playerScoreLevel
        if (playerScoreLevel >= scoreLevelLimits.Length + 1)        // >= 5
            return;

        if (playerScore >= scoreLevelLimits[playerScoreLevel - 1])
        {
            playerScoreLevel++;
        }
    }

    private void MakeFormatList()
    {
        var loadFormats = ResourceDictionary.GetAll<Format>("Formats");

        formatsList.Clear();

        for (int i = 0; i < 5; i++)
        {
            formatsList.Add(new List<Format>());
        }

        foreach(var format in loadFormats)
        {
            formatsList[format.level - 1].Add(format);
        }
    }

    public void GenerateFormat()
    {
        //Ganerate Formats
        for (int i = 0; i < 8; i++)
        {
            //Set GenerateBar
            if (i == 6)
            {
                generateBar.transform.position = new Vector2(0, curFormatPosY);
            }

            //Generate Wall
            if (curFormatPosY >= curWallPosY)
            {
                curWallPosY += wallHeight;
                var leftWall = Instantiate(wallObj, objectsParent); 
                var rightWall = Instantiate(wallObj, objectsParent);

                leftWall.transform.position = new Vector2(- 5.5f,
                    curWallPosY);
                rightWall.transform.position = new Vector2(5.5f,
                    curWallPosY);

                rightWall.transform.eulerAngles = new Vector3(0, 0, -180);
            }

            //Generate Format
            (var level, var index) = SelectFormat();

            var format = Instantiate(formatsList[level][index], objectsParent);
            format.transform.position = new Vector2(0, curFormatPosY);
            curFormatPosY += formatsList[level][index].length + 10f;
        }
    }

    private (int, int) SelectFormat()
    {
        int level = 0, index = 0;

        int probability = Random.Range(1, levelProbabilitys[playerScoreLevel - 1] + 1);

        for (int i = 0; i < levelProbabilitys.Length; i++)
        {
            if (probability <= levelProbabilitys[i])
            {
                level = i;
                break;
            }
        }

        index = Random.Range(0, formatsList[level].Count);
        
        return (level, index);
    }

    public void GameOver()
    {
        isGameStart = false;
        isGaming = false;

        player.GetComponent<Player>().Death();
        player.SetActive(false);

        gameOverUI.SetActive(true);
        resultTimerText.text = "Time : " + string.Format("{0:N1}", playerPlayTime);
        resultScoreText.text = "Score : " + playerScore.ToString();

        SoundManager.instance.SetAudioVolume(SoundType.BGM, 0);
    }

    private IEnumerator SetFade(FadeType fadeType)
    {
        fade.SetActive(true);

        var image = fade.GetComponent<Image>();

        image.color = fadeType == FadeType.FadeIn ?
            new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);

        var color = image.color;

        float colorChage;

        (image.color, colorChage) = fadeType switch
        {
            FadeType.FadeIn => (new Color(0, 0, 0, 1), -0.05f),
            FadeType.FadeOut => (new Color(0, 0, 0, 0), 0.05f),
            _ => (new Color(0, 0, 0, 1), -0.1f)                //Error
        };

        while (0 <= color.a && color.a <= 1)
        {
            color.a += colorChage;
            image.color = color;

            yield return new WaitForSeconds(0.03f);
        }

        fade.SetActive(false);

        yield break;
    }

    private void SetAddScoreText(int score)
    {
        var addScoreTextObj = Instantiate(addScoreText, scoreText.transform);
        addScoreTextObj.GetComponent<TextMeshProUGUI>().text = "+" + score.ToString();
    }

    private void SetAddTimeText(float time)
    {
        var addTimeTextObj = Instantiate(addTimeText, timerText.transform);
        addTimeTextObj.GetComponent<TextMeshProUGUI>().text = "+" + time.ToString();
    }
}
