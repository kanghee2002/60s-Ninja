using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class ClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private readonly List<Action> actions = new();
    public bool isDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        foreach (var action in actions.ToArray()) action();
    }

    public void AddListener(Action action)
    {
        actions.Add(action);
    }

    public void ClearListener()
    {
        actions.Clear();
    }

    public void AddListenerOnly(Action action)
    {
        actions.Clear();
        actions.Add(action);
    }
}
