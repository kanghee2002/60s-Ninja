using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Format : MonoBehaviour
{
    public GameObject scoreCheckObj;
    public int level;
    public int length;
    public int weight;

    private void Start()
    {
        GameObject scoreCheckLine = Instantiate(scoreCheckObj, transform.position + new Vector3(0, 8f * length, 0), transform.rotation);
        scoreCheckLine.GetComponent<ScoreCheck>().score = level;
    }



}
