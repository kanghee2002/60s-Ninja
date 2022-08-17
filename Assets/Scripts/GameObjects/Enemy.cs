using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    [SerializeField]
    private AudioClip deathSound;

    public virtual void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            Instantiate(particle, transform.position, transform.rotation);
            SoundManager.instance.EffectPlay(deathSound);
            InGameManager.instance.AddPlayerScore(3);
            Destroy(gameObject);
        }
    }
}
