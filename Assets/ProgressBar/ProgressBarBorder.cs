using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBarBorder : MonoBehaviour
{
    public Image ProgressBar;

    public Image Border;

    private void Awake()
    {
        MatchProgressBarVisual();
    }

    private void OnValidate()
    {
        MatchProgressBarVisual();
    }

    public void MatchProgressBarVisual()
    {
        Border.sprite = ProgressBar.sprite;
        Border.color = ProgressBar.color;
    }
}
