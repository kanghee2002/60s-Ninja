using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    public GameObject explainText;
    TextMeshProUGUI textMesh;

    public GameObject player;

    public GameObject Arrow;
    public GameObject scoreItem;

    public GameObject timerArrow;
    public GameObject scoreArrow;
    public GameObject settingArrow;

    public GameObject teleportPointer1;
    public GameObject teleportPointer2;

    public GameObject tutorialFormat;

    public GameObject tutorialObstacleFloor;

    int clickCount;
    bool isTutorialObstacleStart;
    bool isTutorialObstacleFinish;
    float timer;
    float playerObstacleStartPosY;

    float floorMoveSpped;

    GameObject scoreObj;
    GameObject arrowObj;

    private void Start()
    {
        floorMoveSpped = 7f;

        clickCount = 0;
        isTutorialObstacleStart = false;
        isTutorialObstacleFinish = false;
        timer = 0;

        textMesh = explainText.transform.GetComponent<TextMeshProUGUI>();

        textMesh.text = "터치하여\n튜토리얼 진행";

        scoreObj = Instantiate(scoreItem, Vector3.zero, transform.rotation);
        arrowObj = Instantiate(Arrow, Vector3.zero, transform.rotation);
        scoreObj.SetActive(false);
        arrowObj.SetActive(false);
    }

    private void Update()
    {
        Explain();

        LiftPlayer();

        UpdatePlayerTimer();        //Prevent GameOver
    }

    void Explain()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (clickCount == 0)
            {
                ExplainPlayer();
            }
            else if (clickCount == 1)
            {
                Destroy(player.transform.GetChild(0).gameObject);
                ExplainScore();
            }
            else if (clickCount == 2)
            {
                scoreArrow.SetActive(false);
                ExplainTimer();
            }
            else if (clickCount == 3)
            {
                timerArrow.SetActive(false);
                ExplainTransport1();
            }
            else if (clickCount == 4)
            {
                teleportPointer1.SetActive(false);
                ExplainTransport2();
            }
            else if (clickCount == 5)
            {
                teleportPointer2.SetActive(false);
                ExplainMove();
            }
            else if (clickCount == 6)
            {
                ExplainScoreItem();
            }

            clickCount++;
        }

        timer += Time.deltaTime;
        if (GameManager.instance.playerScore > 0 && !isTutorialObstacleStart)       //Touching Star
        {
            arrowObj.SetActive(false);
            ExplainObstacle();
            isTutorialObstacleStart = true;
            timer = 0;
            playerObstacleStartPosY = player.transform.position.y;
        }
        else if (isTutorialObstacleStart && timer >= 7f)
        {
            textMesh.text = "";
            timer = 0;
        }
        
        if (player.transform.position.y - playerObstacleStartPosY > 80)
        {
            isTutorialObstacleFinish = true;
        }

        if (isTutorialObstacleFinish)
        {
            tutorialFormat.SetActive(false);
            textMesh.text = "튜토리얼 완료\n우측 상단\n설정 버튼을 눌러\n메인으로";
        }
    }
    void ExplainPlayer()
    {
        textMesh.text = "당신입니다.";
        GameObject plaeyrArrow = Instantiate(Arrow, player.transform);
        plaeyrArrow.transform.localPosition = new Vector3(0, 2, 0);
        plaeyrArrow.transform.eulerAngles = new Vector3(0, 0, 180);
    }

    void ExplainScore()
    {
        scoreArrow.SetActive(true);
        textMesh.text = "점수를 나타냅니다\n별을 먹거나,\n적을 처치하거나,\n" +
            "장애물을 통과하면\n증가합니다.";
    }

    void ExplainTimer()
    {
        timerArrow.SetActive(true);
        textMesh.text = "남은 시간을\n나타냅니다\n계속 올라가\n시간을 늘리세요!\n" +
            "빠르게 올라갈수록\n더 많은 시간이\n주어집니다";
    }
    void ExplainTransport1()
    {
        teleportPointer1.SetActive(true);
        textMesh.text = "클릭해보세요.\n클릭한 곳으로\n표창이 날아갑니다";
    }
    void ExplainTransport2()
    {
        teleportPointer2.SetActive(true);
        textMesh.text = "아무 곳이나\n클릭해보세요.\n표창이 있는 곳으로\n순간이동합니다";
    }

    void ExplainMove()
    {
        textMesh.text = "이동할 수 있는\n방법은 단 하나.\n표창을 날리고\n순간이동하세요."
            + "\n계속해서 터치";
    }


    void ExplainScoreItem()
    {
        Vector3 playerPos = player.transform.position;
        scoreObj.SetActive(true);
        scoreObj.transform.position = playerPos + new Vector3(0, 8, 0);

        arrowObj.SetActive(true);
        arrowObj.transform.position = playerPos + new Vector3(0, 6, 0);

        tutorialObstacleFloor.SetActive(true);
        tutorialObstacleFloor.transform.position = new Vector3(0, -10 + playerPos.y, 0);

        textMesh.text = "점수를 올려주는\n별입니다.\n순간이동하여\n먹어보세요.";
    }

    void ExplainObstacle()
    {
        Vector3 playerPos = player.transform.position;
        tutorialFormat.SetActive(true);
        tutorialFormat.transform.position = new Vector3(0, 20 + playerPos.y, 0);
        
        tutorialObstacleFloor.transform.position = new Vector3(0, -10 + playerPos.y, 0);

        textMesh.text = "이제 장애물들이\n나타날 것입니다.\n가시는 피하고,\n적은 처치하여\n계속해서 올라가세요";
    }

    void LiftPlayer()
    {
        if (isTutorialObstacleStart && !isTutorialObstacleFinish 
            && tutorialObstacleFloor.activeSelf)
        {
            tutorialObstacleFloor.transform.position =
                Vector3.MoveTowards(tutorialObstacleFloor.transform.position,
                new Vector3(0, playerObstacleStartPosY + 10f, 0),
                Time.deltaTime * floorMoveSpped);
        }
    }

    void UpdatePlayerTimer()
    {
        if (GameManager.instance.playerTime < 10)
        {
            GameManager.instance.playerTime += 49;
        }
    }
}
