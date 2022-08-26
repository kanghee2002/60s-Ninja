using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddTextUI : MonoBehaviour
{
    [SerializeField]
    private float fadeOutSpeed;

    [SerializeField]
    private float moveSpeed;

    private TextMeshProUGUI textMesh;
    private RectTransform rect;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        rect = (RectTransform)transform;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        var color = textMesh.color;

        while (color.a > 0)
        {
            textMesh.color = color;
            color.a -= fadeOutSpeed;

            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x,
                rect.anchoredPosition.y + moveSpeed);

            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}
