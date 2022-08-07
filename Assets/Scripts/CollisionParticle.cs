using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour
{
    ParticleSystem parSys;

    private void Start()
    {
        parSys = transform.GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform") || collision.transform.CompareTag("Wall"))
        {
            /*
            Vector2 colPos = collision.contacts[0].point;
            Vector2 myPos = transform.position;

            float xdiff = colPos.x - myPos.x;
            float ydiff = colPos.y - myPos.y;

            print(xdiff);
            print(ydiff);

            if (Mathf.Abs(xdiff) > Mathf.Abs(ydiff))
            {
                if (xdiff > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                    print('L');
                }
                if (xdiff < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
                    print('R');
                }
            }

            else
            {
                if (ydiff > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    print('U');
                }
                if (ydiff < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    print('D');
                }
            }
            */ 
            parSys.Play();
        }
    }
}
