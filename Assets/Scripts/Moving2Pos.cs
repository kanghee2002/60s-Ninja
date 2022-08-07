using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving2Pos : MonoBehaviour
{
    int toGo;
    Vector3 currentPos;
    Vector3 Pos1;
    Vector3 Pos2;
    Vector3 dir;
    public float movingSpeed;

    public GameObject wireObj;
    public bool isRotate;
    public float rotateSpped;
    
    void Start()
    {
        InstantiateWire();
    }

    void FixedUpdate()
    {
        if (isRotate)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpped) * 180 * Time.deltaTime);
        }

        Move2Pos();
    }

    void InstantiateWire()
    {
        Pos1 = transform.GetChild(0).position;
        Pos2 = transform.GetChild(1).position;
        dir = (Pos1 - Pos2);

        Vector3 center = (Pos1 + Pos2) / 2;
        center.z = 1f;

        float angle = GetAngle(Pos1, Pos2);
        Quaternion rotation = Quaternion.Euler(0f, 0f, 90f + angle);
        GameObject wire = Instantiate(wireObj, center, rotation);
        wire.transform.localScale = new Vector3(1, dir.magnitude, 1);
        wire.transform.parent = transform.parent.gameObject.transform;


        toGo = 1;
        transform.position = Pos2;
    }

    void Move2Pos()
    {
        currentPos = transform.position;

        if (toGo == 1)
        {
            if ((currentPos - Pos1).sqrMagnitude < 1)
            {
                toGo = 2;
            }
            transform.position += (dir * movingSpeed);
        }
        else if (toGo == 2)
        {
            if ((currentPos - Pos2).sqrMagnitude < 1)
            {
                toGo = 1;
            }
            transform.position += (-dir * movingSpeed);
        }
    }

    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

}
