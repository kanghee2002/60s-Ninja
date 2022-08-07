using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public GameObject knifeObj;
    public int throwPower;
    public int maxKnifeNum;
    int curKnifeNum;

    bool isSliding;
    public float slidingSpeed;

    List<GameObject> knives = new List<GameObject>();

    public GameObject flashParticle;
    public GameObject deathParticle;

    public AudioClip landingSound;
    public AudioClip teleportSound;
    public AudioClip deathSound;

    bool isStart;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        GameManager.instance.player = gameObject;
    }
    void Start()
    {
        curKnifeNum = maxKnifeNum;
        isSliding = false;
        isStart = false;
    }

    void Update()
    {
        Throw();
    }

    private void FixedUpdate()
    {
        Slide();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            SoundManager.instance.EffectPlay(landingSound);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            isSliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Platform"))
        {
            isSliding = false;
        }

    }


    void Throw()
    {
        if (Input.touchCount > 0)    //prevent ui touch
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
        }

        if (Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began 
            && curKnifeNum > 0 && GameManager.instance.isGaming)
        {
            if (!isStart)
            {
                GameManager.instance.GameStart();
                isStart = true;
            }

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
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began 
            && curKnifeNum <= 0)
        {
            SoundManager.instance.EffectPlay(teleportSound);
            GameObject flashParticleObj = Instantiate(flashParticle, transform.position, transform.rotation);
            transform.position = knives[0].transform.position;
            rigid.velocity = Vector2.zero;
            Destroy(knives[0]);
            knives.RemoveAt(0);

            if (knives.Count == 0)
                curKnifeNum = maxKnifeNum;
        }
    }

    void Slide()
    {
        if (isSliding)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
        }
    }
}
