using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingShuriken : Shuriken
{
    public float speed;
    Sight2D sight;
    Rigidbody2D rigid;
    bool isWall;
    public float setStopTime;
    float curTime;

    void Start()
    {
        isWall = false;
        sight = transform.GetComponent<Sight2D>();
        rigid = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ChaseTarget();
        StopChase();
    }

    void ChaseTarget()
    {

        if (sight.target != null && !isWall)
        {
            curTime = 0;
            
            Transform target = sight.target;
            Vector2 dir = new Vector2(target.position.x - transform.position.x,
                target.position.y - transform.position.y);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime);
            rigid.velocity = dir.normalized * speed * Time.deltaTime;
        }
    }

    void StopChase()
    {
        if (sight.target == null)
        {
            curTime += Time.deltaTime;
            if (curTime >= setStopTime)
            {
                rigid.gravityScale = 1;
            }
        }
        if (isWall)
        {
            rigid.gravityScale = 1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isWall = true;
        }
    }
}
