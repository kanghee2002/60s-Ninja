using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Rigidbody2D rigid;

    public float rotationSpeed;

    public AudioClip knifeShot;

    bool isStuck = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

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
            if (SoundManager.instance != null)
            {
                SoundManager.instance.EffectPlay(knifeShot);
            }
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
            //InGameManager.instance.GenerateFormat();
        }
    }

    void Stuck()
    {
        if (isStuck)
        {
            rigid.velocity = Vector2.zero;
        }
    }

    /*
    void Rotate()
    {
        if (isStuck)
        {
            transform.Rotate(new Vector3(0, 0, 1) * 180 * rotationSpeed * Time.deltaTime);
        }
    }
    */
}
