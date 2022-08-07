using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject player;
    public float controlPos;

    int screenWidth;
    int screenHeight;
    Camera m_camera;

    private void Start()
    {
        //screenWidth = Screen.width;
        //screenHeight = Screen.height;
        m_camera = transform.GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        if (player != null) 
        {
                float SmoothY = Mathf.Lerp(transform.position.y, player.transform.position.y + controlPos, Time.deltaTime * 6f);
                transform.position = new Vector3(transform.position.x, SmoothY, -10);
        } 
        else
        {
            player = GameManager.instance.player;
        }

    }

    private void Update()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        m_camera.orthographicSize = (float)11.4 * screenHeight / (screenWidth * 2);
    }
}
