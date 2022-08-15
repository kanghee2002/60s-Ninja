using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField]
    private GameObject shotParticle;

    [SerializeField]
    private AudioClip knifeWallShot;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(shotParticle, transform.position, transform.rotation);
        SoundManager.instance.EffectPlay(knifeWallShot);
        Destroy(gameObject);
    }
}

