using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreItem : MonoBehaviour
{
    public AudioClip itemPick;
    public GameObject particle;

    private void Start()
    {
        int x = Random.Range(1, 3);
        if (SceneManager.GetActiveScene().name == "InGame" && x > 1)
        {
            Destroy(gameObject);
        }
    }

    public int score = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(particle, transform.position, transform.rotation);
            //InGameManager.instance.AddPlayerScore(score);
            SoundManager.instance.EffectPlay(itemPick);
            Destroy(gameObject);
        }
    }
}
