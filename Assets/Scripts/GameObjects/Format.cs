using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Format : MonoBehaviour
{
    [SerializeField]
    private ScoreCheck scoreCheckObj;

    public int level;

    public int length;

    private void Start()
    {
        GameObject scoreCheckLine = Instantiate(scoreCheckObj.gameObject, 
            new Vector2(0, transform.position.y + length + 1), transform.rotation);
        scoreCheckLine.transform.parent = transform;
        scoreCheckLine.GetComponent<ScoreCheck>().score = level;
    }



}
