using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private GameObject knifeShotParticle;

    private bool isStuck = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Stuck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isStuck = true;
        }

        if ( collision.gameObject.CompareTag("Platform")
            || collision.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(knifeShotParticle, transform.position, transform.rotation);
            SoundManager.instance.PlaySFX("HitKnife");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isStuck = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GenerateBar")
        {
            InGameManager.instance.GenerateFormat();
        }
    }

    void Stuck()
    {
        if (isStuck)
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
