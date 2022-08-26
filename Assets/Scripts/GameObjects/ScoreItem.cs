using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreItem : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    private int score;

    private void Start()
    {
        //Tutorial Exception
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            score = 1;
            return;
        }

        Init();
    }

    private void Init()
    {
        SetScore();
        DestroyRandom();
    }

    private void SetScore()
    {
        var parent = transform.parent;

        while (parent.parent != null)
        {
            if (parent.TryGetComponent<Format>(out var format))
            {
                score = format.level;
                break;
            }
            parent = parent.parent;
        }

    }

    private void DestroyRandom()
    {
        int x = Random.Range(1, 3);
        if (x > 1) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(particle, transform.position, transform.rotation);
            InGameManager.instance.AddPlayerScore(score);
            InGameManager.instance.AddPlayerTime(score);
            SoundManager.instance.PlaySFX("PickItem");
            Destroy(gameObject);
        }
    }
}
