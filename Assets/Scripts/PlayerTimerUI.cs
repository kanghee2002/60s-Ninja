using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimerUI : MonoBehaviour
{   
    float playerTime;
    RectTransform rectTransform;
    Image MoonUI;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        MoonUI = transform.parent.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        playerTime = InGameManager.instance.playerTime;   //60 -> 0
        rectTransform.anchoredPosition = new Vector3(45 - playerTime * 45 / 60,
            -45 + playerTime * 45 / 60, 0);        //0, 0 -> 45, -45
        MoonUI.color = new Color(1, playerTime / 20f, playerTime / 20f, 1);      // After 20s  1 -> 0
    }
}
