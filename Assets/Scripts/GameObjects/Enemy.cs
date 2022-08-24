using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;
    
    public virtual void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            Instantiate(particle, transform.position, transform.rotation);
            SoundManager.instance.PlaySFX("DieEnemy");
            InGameManager.instance.AddPlayerScore(3);
            Destroy(gameObject);
        }
    }
}
