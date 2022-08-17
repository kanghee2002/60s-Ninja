using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreItem : MonoBehaviour
{
    [SerializeField]
    private AudioClip itemPick;

    [SerializeField]
    private GameObject particle;

    public int score { get; set; }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        score = Random.Range(1, 5);

        int x = Random.Range(1, 3);
        if (SceneManager.GetActiveScene().name == "InGame" && x > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(particle, transform.position, transform.rotation);
            InGameManager.instance.AddPlayerScore(score);
            SoundManager.instance.EffectPlay(itemPick);
            Destroy(gameObject);
        }
    }
}
