using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SpaseShooter;

public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
{
    private Image blocker;
    private Action<Vector2> m_OnClickAction;

    private void Start()
    {
        blocker = GetComponent<Image>();
    }
    public void Activate(Action<Vector2> mouseAction)
    {
        blocker.enabled = true;
        m_OnClickAction = mouseAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        blocker.enabled = false;
        m_OnClickAction(eventData.pressPosition);
        m_OnClickAction = null;
    }
}
