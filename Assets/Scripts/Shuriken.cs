using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject particle;
    public AudioClip knifeWallShot;
    public float livingTime;

    bool isSoundPlayed;
    void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        isSoundPlayed = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") 
            || collision.gameObject.CompareTag("Platform"))
        {
            StopFlying();
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform")
            || collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isSoundPlayed)
            {
                SoundManager.instance.EffectPlay(knifeWallShot);
                isSoundPlayed = true;
            }
        }
    }
    void StopFlying()
    {
        Instantiate(particle, transform.position, transform.rotation);
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;
        Invoke("DestroyGameObject", livingTime);

    }

    void DestroyGameObject()
    {
        Instantiate(particle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

