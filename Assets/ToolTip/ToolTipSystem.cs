using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ToolTipSystem : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/ToolTip System")]
    public static void AddLinearProgressBar()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/ToolTip Canvas"));
    }
#endif
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
        _current.ToolTip.gameObject.SetActive(true);
        _current.ToolTip.transform.localScale = new Vector3(0, 0, 0);
        _current.ToolTip.transform.DOScale(1f, 0.2f);
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
        DOTween.Sequence().Append(_current.ToolTip.transform.DOScale(0f, 0.2f / _current.CloseSpeedFactor)).AppendCallback(() => { _current.ToolTip.gameObject.SetActive(false); });
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
