using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject particle;
    public AudioClip deathSound;
    public virtual void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            Instantiate(particle, transform.position, transform.rotation);
            SoundManager.instance.EffectPlay(deathSound);
            GameManager.instance.AddPlayerScore(3);
            Destroy(gameObject);
        }
    }
}
