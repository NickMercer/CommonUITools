using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header;
    public string Content;
    public float DelayTime = 0.5f;

    private Coroutine _showCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _showCoroutine = StartCoroutine(ShowToolTip());
    }

    public void OnMouseEnter()
    {
        _showCoroutine = StartCoroutine(ShowToolTip());
    }

    private IEnumerator ShowToolTip()
    {
        yield return new WaitForSeconds(DelayTime);
        ToolTipSystem.Show(Content, Header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(_showCoroutine);
        ToolTipSystem.Hide();
    }

    public void OnMouseExit()
    {
        StopCoroutine(_showCoroutine);
        ToolTipSystem.Hide();
    }
}
