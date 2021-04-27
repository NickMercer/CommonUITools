using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI HeaderField;

    public TextMeshProUGUI ContentField;

    public LayoutElement LayoutElement;

    public int CharacterWrapLimit;

    public int ScreenEdgePadding = 10;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            UpdateLayout();
        }

        var position = Input.mousePosition;

        var pivotX = Screen.width - position.x > rectTransform.rect.width + ScreenEdgePadding ? 0 : 1;
        var pivotY = Screen.height - position.y > rectTransform.rect.height + ScreenEdgePadding ? 0 : 1;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }


    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            HeaderField.gameObject.SetActive(false);
        }
        else
        {
            HeaderField.gameObject.SetActive(true);
            HeaderField.text = header;
        }

        ContentField.text = content;
        
        UpdateLayout();
    }


    private void UpdateLayout()
    {
        var headerLength = HeaderField.text.Length;
        var contentLength = ContentField.text.Length;

        LayoutElement.enabled = (headerLength > CharacterWrapLimit || contentLength > CharacterWrapLimit) ? true : false;
    }
}
