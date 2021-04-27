using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{

    private static ToolTipSystem _current;

    public ToolTip ToolTip;

    private static Coroutine _fadeInCoroutine;
    private static Coroutine _fadeOutCoroutine;

    public float GrowSpeed = 0.01f;
    public float CloseSpeedFactor = 5;

    private void Awake()
    {
        _current = this;
    }

    public static void Show(string content, string header = "")
    {
        _current.ToolTip.SetText(content, header);
        if (_fadeOutCoroutine != null)
        {
            _current.StopCoroutine(_fadeOutCoroutine);
        }
        _fadeInCoroutine = _current.StartCoroutine(GrowIn());
    }

    private static IEnumerator GrowIn()
    {
        _current.ToolTip.gameObject.SetActive(true);
        var rect = _current.ToolTip.GetComponent<RectTransform>();
        rect.localScale = new Vector3(0, 0, 0);
        while (rect.localScale.x < Vector3.one.x)
        {
            rect.localScale += new Vector3(_current.GrowSpeed, _current.GrowSpeed, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    public static void Hide()
    {
        if(_fadeInCoroutine != null)
        {
            _current.StopCoroutine(_fadeInCoroutine);
        }
        _fadeOutCoroutine = _current.StartCoroutine(ShrinkOut());
    }

    private static IEnumerator ShrinkOut()
    {
        var rect = _current.ToolTip.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 0);
        while (rect.localScale.x > Vector3.zero.x)
        {
            rect.localScale -= new Vector3(_current.GrowSpeed * _current.CloseSpeedFactor, _current.GrowSpeed * _current.CloseSpeedFactor, 0);
            yield return new WaitForEndOfFrame();
        }
        _current.ToolTip.gameObject.SetActive(false);
    }
}
