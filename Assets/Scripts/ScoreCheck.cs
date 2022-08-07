using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCheck : MonoBehaviour
{
    public int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            //InGameManager.instance.AddPlayerScore(score);
            //InGameManager.instance.AddPlayerTime(score);
            //InGameManager.instance.startBonusCheckTime = true;
            Destroy(gameObject);
        }
    }
}
