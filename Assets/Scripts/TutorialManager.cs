using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private ButtonManager buttonManager;

    [SerializeField]
    private TextMeshProUGUI explainText;

    [SerializeField]
    private GameObject[] tutorialObjs;
    //ScoreArrow, TimerArrow, SettingArrow,
    //TeleportPointer1, TeleportPointer2 

    [SerializeField]
    private GameObject arrowObj;

    [SerializeField]
    private GameObject scoreItemObj;

    [SerializeField]
    private GameObject tutorialFormat;

    [SerializeField]
    private GameObject tutorialObstacleFloor;

    private GameObject player;

    private int clickCount;
    private bool isTutorialObstacleStart;
    private bool isTutorialObstacleFinish;
    private float timer;
    private float playerObstacleStartPosY;
    private float floorMoveSpped;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        player = InGameManager.instance.player;

        floorMoveSpped = 7f;

        clickCount = 0;
        isTutorialObstacleStart = false;
        isTutorialObstacleFinish = false;
        timer = 0;

        explainText.text = "터치하여\n튜토리얼 진행";

        scoreItemObj.transform.position = Vector2.zero;
        scoreItemObj.SetActive(false);
        arrowObj.transform.position = Vector2.zero;
        arrowObj.SetActive(false);
    }

    private void Update()
    {
        ExplainGame();

        LiftPlayer();

        UpdatePlayerTimer();        //Prevent GameOver
    }

    private void ExplainGame()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            switch (clickCount)
            {
                case 0:
                    explainText.text = "당신입니다.";
                    arrowObj.SetActive(true);
                    arrowObj.transform.parent = player.transform;
                    arrowObj.transform.localPosition = new Vector3(0, 2, 0);
                    arrowObj.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 1:
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    tutorialObjs[0].SetActive(true);
                    explainText.text = "점수를 나타냅니다";
                    break;
                case 2:
                    tutorialObjs[0].SetActive(false);
                    tutorialObjs[1].SetActive(true);
                    explainText.text = "남은 시간을\n나타냅니다";
                    break;
                case 3:
                    tutorialObjs[1].SetActive(false);
                    tutorialObjs[2].SetActive(true);
                    explainText.text = "클릭해보세요\n클릭한 곳으로\n표창이 날아갑니다";
                    break;
                case 4:
                    tutorialObjs[2].SetActive(false);
                    tutorialObjs[3].SetActive(true);
                    explainText.text = "아무 곳이나\n클릭해보세요\n표창이 있는 곳으로\n순간이동합니다";
                    break;
                case 5:
                    tutorialObjs[3].SetActive(false);

                    Vector3 playerPos = player.transform.position;
                    scoreItemObj.SetActive(true);
                    scoreItemObj.transform.position = playerPos + new Vector3(0, 8, 0);

                    arrowObj.SetActive(true);
                    arrowObj.transform.position = playerPos + new Vector3(0, 10, 0);
                    arrowObj.transform.parent = null;

                    tutorialObstacleFloor.SetActive(true);
                    tutorialObstacleFloor.transform.position = new Vector3(0, -10 + playerPos.y, 0);

                    explainText.text = "점수를 올려주는\n별입니다\n" +
                        "순간이동하여\n먹어보세요";
                    break;
            }
            clickCount++;
        }

        timer += Time.deltaTime;
        if (InGameManager.instance.playerScore > 0 && !isTutorialObstacleStart)       //Touching Star
        {
            arrowObj.SetActive(false);
            ExplainObstacle();
            isTutorialObstacleStart = true;
            timer = 0;
            playerObstacleStartPosY = player.transform.position.y;
        }
        else if (isTutorialObstacleStart && timer >= 7f)
        {
            explainText.text = "";
            timer = 0;
        }
        
        if (player.transform.position.y - playerObstacleStartPosY > 80)
        {
            isTutorialObstacleFinish = true;
        }

        if (isTutorialObstacleFinish)
        {
            tutorialFormat.SetActive(false);
            explainText.text = "튜토리얼 완료\n우측 상단\n설정 버튼을 눌러\n메인으로";
            tutorialObstacleFloor.transform.position = new Vector3(0, playerObstacleStartPosY + 50f, 0);
        }
    }

    private void ExplainObstacle()
    {
        Vector3 playerPos = player.transform.position;
        tutorialFormat.SetActive(true);
        tutorialFormat.transform.position = new Vector3(0, 20 + playerPos.y, 0);
        
        tutorialObstacleFloor.transform.position = new Vector3(0, -10 + playerPos.y, 0);

        explainText.text = "가시는 피하고\n적은 처치하여\n계속 올라가세요";
    }

    private void LiftPlayer()
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

    private void UpdatePlayerTimer()
    {
        if (InGameManager.instance.playerTime < 10)
        {
            InGameManager.instance.AddPlayerTime(20);
        }
    }
}
