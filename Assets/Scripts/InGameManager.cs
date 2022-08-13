using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum FadeType { FadeIn, FadeOut };

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance = null;


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

    public int playerScoreLevel { get; private set; }    //0, 1, 2, 3, 4

    public float playerTime { get; private set; }

    public bool isGameStart { get; private set; }

    public bool isGaming { get; set; }


    [Header("UI")]
    [SerializeField]
    private ButtonManager buttonManager;

    [SerializeField]
    private GameObject gameOverUI;


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

    private List<List<Format>> formatsList = new();


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
        MakeFormatList();
        generateBar = transform.GetChild(0).gameObject;
        buttonManager.InitInGame();
    }

    private void Update()
    {
        CountPlayerTime();
        SetText();
    }

    public void StartGame()
    {
        isGameStart = true;
        isGaming = true;
        SoundManager.instance.StartBGM(SceneType.InGame);
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

        generateBar.SetActive(true);

        playerScore = 0;
        playerPlayTime = 0;
        playerScoreLevel = 0;
        playerTime = 60f;
        isGameStart = false;
        isGaming = false;
    }

    private void CountPlayerTime()
    {
        if (isGaming)
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

    /*public void AddPlayerTime(float score)
    {
        float timeMagnification = 3f;
        float addTime = timeMagnification * score + 3 - bonusCheckTime;
        if (score >= 4) addTime += (score - 3);

        if (addTime < 0) addTime = 0;
        playerTime += addTime;
        bonusCheckTime = 0;
        if (playerTime > 60f)
        {
            playerTime = 60f;
        }
    }*/

    /* void StartBonusCheckTime()
     {
         if (startBonusCheckTime)
         {
             bonusCheckTime += Time.deltaTime;
         }

     }*/

    /*void ScoreLevelCheck()
    {
        if (playerScoreLevel == HighestFormatLevel - 1)
            return;

        if (playerScore > scoreLevelLimits[playerScoreLevel])
        {
            playerScoreLevel++;
        }
    }*/

    void MakeFormatList()
    {
        var loadFormats = Resources.LoadAll<Format>("Formats");

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

    /*public void GenerateFormat()
    {
        //Generate Wall
        transform.position = new Vector3(transform.position.x, transform.position.y + 100, 0);
        Instantiate(wallObj, new Vector3(transform.position.x - 5.5f, transform.position.y, 0), transform.rotation);
        GameObject WallR = Instantiate(wallObj, new Vector3(transform.position.x + 5.5f, transform.position.y, 0), transform.rotation);
        WallR.transform.localScale += new Vector3(-2, 0, 0);

        //Ganerate Formats
        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
            {
                i += overFormatLength;
                overFormatLength = 0;
            }

            int key = SelectFormat();
            int formatLength = formatsToGenerate[key].GetComponent<Format>().length;
            GameObject Format = Instantiate(formatsToGenerate[key], new Vector3(0,
                transform.position.y - 50 + 12.5f * i, 0), transform.rotation);

            int x = Random.Range(0, 2);
            if (x >= 1)
            {
                Format.transform.localScale += new Vector3(-2, 0, 0);
            }

            i += (formatLength - 1);

            if (i >= 8)
            {
                overFormatLength = i - 7;
            }
        }
    }*/
    /*int SelectFormat()
    {
        int level =  SelectFormatLevel();
        int randomx;
        if (level == 1)
        {
            randomx = Random.Range(0, formatsLevelCutLines[level - 1]);
        }
        else if (level == HighestFormatLevel)
        {
            randomx = Random.Range(formatsLevelCutLines[level - 2], formatsToGenerate.Count);
        }
        else
        {
            randomx = Random.Range(formatsLevelCutLines[level - 2], formatsLevelCutLines[level - 1]);
        }
        return randomx;
    }*/

    /*int SelectFormatLevel()
    {
        int randomx = Random.Range(1,levelProbabilitys[playerScoreLevel]);
        for (int i = 0; i < levelProbabilitys.Count; i++)
        {
            if (randomx <= levelProbabilitys[i])
            {
                return i + 1;           //i == choosed level
            }
        }
        print("SelectFormatLevelError");
        return -1;
    }
    public void AddPlayerScore(int score)
    {
        playerScore += score;
        ScoreLevelCheck();
    }*/

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

            yield return null;
        }

        fade.SetActive(false);

        yield break;
    }
}
