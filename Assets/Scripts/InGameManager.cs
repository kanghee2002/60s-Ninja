using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance = null;

    //GameObject
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private GameObject wallObj;
    [SerializeField]
    private GameObject generateBar;

    //Format
    [SerializeField]
    private List<GameObject> formats = new List<GameObject>();           //Inspector

    private List<List<GameObject>> formatsList = new List<List<GameObject>>(); //First index = level

    List<int> scoreLevelLimits = new List<int>();
    List<int> levelProbabilitys = new List<int>();      //1, 1+4=5, 1+4+9=14, 30, 55

    private int overFormatLength = 0;

    //InGame
    public GameObject player { get; private set; }
    public int playerScore { get; private set; }

    public float playerPlayTime { get; private set; }

    public int playerScoreLevel { get; private set; }    //0, 1, 2, 3, 4

    public float playerTime { get; private set; }

    public bool isStart { get; private set; }

    public bool isGaming { get; private set; }

    //Etc
    [SerializeField]
    private AudioClip playerDeathSound;
    [SerializeField]
    private GameObject playerDeathParticle;
    [SerializeField]
    private GameObject gameOverUI;

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


    public void InitFormat()
    {
        MakeFormatList();
        MakeLevelProbabilitys();
        generateBar = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        /*UnityEngine.PlayerLoop.Initialization();
        playerTimer();
        StartBonusCheckTime();*/
    }

    public void GameStart()
    {
        isStart = true;
        SoundManager.instance.BGMInGameStart();
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            generateBar.SetActive(true);
        }
    }

    [ContextMenu("InitGame")]
    public void InitGame()
    {
        player = Instantiate(playerObj, Vector3.zero, transform.rotation);
        player.GetComponent<Player>().Init();
    }

    /*public void Initialization()
    {
        if (isInitialized)
        {
            return;
        }

        transform.position = Vector3.zero;
        playerScore = 0;
        overFormatLength = 0;
        playerScoreLevel = 0;
        playerTime = 60f;
        playerPlayTime = 0f;
        isStart = false;
        isGaming = true;
        isInitialized = true;
    }*/

    void playerTimer()
    {
        if (isStart)
        {
            playerPlayTime += Time.deltaTime;
            //print(playerPlayTime);
            playerTime -= Time.deltaTime;
            if (playerTime < 0)
            {
                GameOver();
                isStart = false;
            }
        }
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

    void MakeLevelProbabilitys()
    {
        int sum = 0;
        for(int i = 1; i < formatsList.Count + 1; i++)
        {
            int x = (int)Mathf.Pow(i, 2);
            sum += x;
            levelProbabilitys.Add(sum);            //1, 1+4=5, 1+4+9=14, 30, 55
        }
    }

    void MakeFormatList()
    {
        foreach(var format in formats)
        {
            if (format == null)
            {
                continue;
            }

            Format f = format.GetComponent<Format>();

            formatsList[f.level].Add(format);
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
        isStart = false;
        SoundManager.instance.EffectPlay(playerDeathSound);
        Instantiate(playerDeathParticle,
            player.transform.position, player.transform.rotation);
        gameOverUI.SetActive(true);
        player.SetActive(false);
        SoundManager.instance.audioSourceBGM.volume = 0;

    }
}
