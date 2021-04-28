using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    [Header("Progress Bar Values")]
    public int Minimum;

    public int Maximum;

    public int Current;
    
    private Image _mask;

    [Header("Progress Bar Fill")]
    public Image Fill;

    public Color Color;

    private void Awake()
    {
        SetPrivateFields();
    }

    private void OnValidate()
    {
        SetPrivateFields();
    }

    private void Update()
    {
        GetCurrentFill();
    }


    private void SetPrivateFields()
    {
        _mask = GetComponentInChildren<Mask>().GetComponent<Image>();
    }

    private void GetCurrentFill()
    {
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;
        var fillAmount = currentOffset / maximumOffset;

        _mask.fillAmount = fillAmount;
        Fill.color = Color;
    }
}
