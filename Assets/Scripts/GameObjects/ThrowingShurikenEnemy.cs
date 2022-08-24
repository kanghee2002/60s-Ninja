using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingShurikenEnemy : Enemy
{
    [SerializeField]
    private GameObject shurikenObj;

    [SerializeField]
    private GameObject particleExclamtion;

    [SerializeField]
    private float throwingPower;

    [SerializeField]
    private float coolSetTime;

    [SerializeField]
    private float firstDetectSetTime;

    [SerializeField]
    private float coolCurTime;

    [SerializeField]
    private float firstDetectCurTime;

    [SerializeField]
    private bool isCool;

    [SerializeField]
    private bool isFirstDetect;     //false -> throw

    private Sight2D sight;

    private SpriteRenderer spriteRenderer;

    private bool isExclamation;


    private void Start()
    {
        isCool = true;
        isFirstDetect = true;
        coolCurTime = 0;
        firstDetectCurTime = 0;
        sight = transform.GetComponent<Sight2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();

    }
    private void FixedUpdate()
    {
        SetDetectType();
        TurnToPlayer();
    }

    public override void Attack()
    {
        if (sight.target != null && !isCool)
        {
            Transform playerT = sight.target;
            Vector2 dir = (playerT.transform.position - transform.position).normalized;

            GameObject shuriken = Instantiate(shurikenObj, transform.position, transform.rotation);
            Vector2 setDir = new Vector2(playerT.position.x - transform.position.x,
                playerT.position.y - transform.position.y);

            float angle = Mathf.Atan2(setDir.y, setDir.x) * Mathf.Rad2Deg - 180 + 45;
            shuriken.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Rigidbody2D shurikenRigid = shuriken.GetComponent<Rigidbody2D>();
            shurikenRigid.AddForce(dir * throwingPower, ForceMode2D.Impulse);
        }
    }

    private void SetDetectType()
    {
        if (!isFirstDetect)
        {
            Attack();
            isCool = CheckCoolTime();
        }
        else
        {
            isFirstDetect = CheckfirstDetectTime();
            if (!isFirstDetect)
            {
                isCool = false;
            }
        }
    }


    private bool CheckCoolTime()
    {
        if (sight.target != null) 
        {
            coolCurTime += Time.deltaTime;
            if (coolCurTime >= coolSetTime)
            {
                coolCurTime = 0;
                return false;
            }
            return true;
        }
        else 
        {
            coolCurTime = 0;
            isFirstDetect = true;
            return true;
        }
    }
    private bool CheckfirstDetectTime()
    {
        if (sight.target != null)
        {
            if (isExclamation)
            {
                GameObject particleExclamationObj = Instantiate(particleExclamtion, transform.position, transform.rotation);
                particleExclamationObj.transform.parent = gameObject.transform;
                particleExclamationObj.transform.localScale = new Vector3(1, 1, 1);
                SoundManager.instance.PlaySFX("NoticeEnemy");
            }
            isExclamation = false;
            firstDetectCurTime += Time.deltaTime;
            if (firstDetectCurTime >= firstDetectSetTime)
            {
                firstDetectCurTime = 0;
                return false;
            }
            return true;
        }
        else
        {
            isExclamation = true;
            firstDetectCurTime = 0;
            return true;
        }
    }

    private void TurnToPlayer()
    {
        if (sight.target != null)
        {
            Transform playerT = sight.target;
            if (playerT.position.x > transform.position.x)      //E P
            {
                spriteRenderer.flipX = false;
                if (transform.lossyScale.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (playerT.position.x < transform.position.x)     //P E
            {
                spriteRenderer.flipX = true;
                if (transform.lossyScale.x < 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }
}
