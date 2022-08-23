using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;

    [Header("GameObject")]
    [SerializeField]
    private GameObject knifeObj;

    [Header("Stat")]
    [SerializeField]
    private int throwPower;

    [SerializeField]
    private float slidingSpeed;

    [SerializeField]
    private int maxKnifeNum;

    [Header("Particle")]
    [SerializeField]
    private GameObject flashParticle;

    [SerializeField]
    private GameObject deathParticle;

    [Header("Sound")]
    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private AudioClip landingSound;

    [SerializeField]
    private AudioClip teleportSound;


    private bool isSliding;

    private int curKnifeNum;
    private List<GameObject> knives = new List<GameObject>();


    public void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        curKnifeNum = maxKnifeNum;
        isSliding = false;
    }

    private void Update()
    {
        ThrowKnife();
    }

    private void FixedUpdate()
    {
        Slide();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !InGameManager.instance.debugMode)
        {
            InGameManager.instance.GameOver();
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            SoundManager.instance.EffectPlay(landingSound);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isSliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isSliding = false;
        }

    }

    private void ThrowKnife()
    {
        if (Input.touchCount > 0)    
        {   
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {   //Prevent UI touch
                return;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!InGameManager.instance.isGameStart)
            {
                InGameManager.instance.StartGame();
            }

            if (!InGameManager.instance.isGaming)
            {
                return;
            }

            if (curKnifeNum > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                Vector2 playerPos = transform.position;
                Vector2 direction = (touchPos - playerPos).normalized;

                GameObject knife = Instantiate(knifeObj, playerPos, transform.rotation);
                knives.Add(knife);
                Rigidbody2D knifeRigid = knife.GetComponent<Rigidbody2D>();
                knifeRigid.AddForce(direction * throwPower, ForceMode2D.Impulse);

                curKnifeNum--;
            }
            else
            {
                if (SoundManager.instance != null)
                {
                    SoundManager.instance.EffectPlay(teleportSound);
                }
                Instantiate(flashParticle, transform.position, transform.rotation);
                transform.position = knives[0].transform.position;
                rigid.velocity = Vector2.zero;
                Destroy(knives[0]);
                knives.RemoveAt(0);

                if (knives.Count == 0)
                {
                    curKnifeNum = maxKnifeNum;
                }

            }
        }
    }

    private void Slide()
    {
        if (isSliding)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
        }
    }

    public void Death()
    {
        SoundManager.instance.EffectPlay(deathSound);
        Instantiate(deathParticle, transform.position, transform.rotation);
    }
}
