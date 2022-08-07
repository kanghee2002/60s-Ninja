using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverBar : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y + speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(collision.gameObject);
        
    }
}
