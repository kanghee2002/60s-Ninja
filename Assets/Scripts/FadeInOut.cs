using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public int x;

    Image image;
    Color color;

    public IEnumerator FadeIn(string scene)     //op - > tr
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        color = image.color;

        while (color.a > 0)
        {
            color.a -= 0.1f;
            image.color = color;

            yield return null;
        }

        if (scene != null)
        {
            SceneManager.LoadScene(scene);
        }
    }
    public IEnumerator FadeOut(string scene)     //tr -> op
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0);
        color = image.color;

        while (color.a < 1)
        {
            color.a += 0.1f;
            image.color = color;

            yield return null;
        }

        if (scene != null)
        {
            SceneManager.LoadScene(scene);
        }
    }

    private void Update()
    {
        if (x == 1)
        {
            image = GetComponent<Image>();
            color = image.color;

            if (color.a > 0)
            {
                color.a -= 0.03f;
                image.color = color;

            }

            else
            {
                Destroy(gameObject);
            }

        }
    }
}
