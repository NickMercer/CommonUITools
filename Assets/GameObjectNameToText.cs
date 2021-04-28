using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class GameObjectNameToText : MonoBehaviour
{
    public GameObject Parent;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(_text != null)
        {
            _text = GetComponent<TMP_Text>();
        }
        _text.text = Parent.name;
#endif
    }
}
