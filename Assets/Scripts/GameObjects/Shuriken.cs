using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField]
    private GameObject shotParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(shotParticle, transform.position, transform.rotation);
        SoundManager.instance.PlaySFX("HitKnife");
        Destroy(gameObject);
    }
}

