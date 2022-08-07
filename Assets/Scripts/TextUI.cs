using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    public int code;

    void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        code1();
        code2();
        code3();
    }

    void Update()
    {
        code1();
        code4();
    }

    void code1()
    {
        if (code == 1)
        {
            textMesh.text = GameManager.instance.playerScore.ToString();
        }
    }
    void code2()
    {
        if (code == 2)
        {
            textMesh.text = "Score : " + GameManager.instance.playerScore.ToString();
        }
    }
    void code3()
    {
        if (code == 3)
        {
            textMesh.text = "Time : " + string.Format("{0:N1}", GameManager.instance.playerPlayTime);
        }
    }

    void code4()
    {
        if (code == 4)
        {
            textMesh.text = string.Format("{0:N1}", GameManager.instance.playerTime);
        }
    }
}
