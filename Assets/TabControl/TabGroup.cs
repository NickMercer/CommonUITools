using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TabGroup : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Tab Control")]
    public static void AddLinearProgressBar()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/Tab Control"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public List<TabButton> TabButtons;
    public Sprite TabIdle;
    public Sprite TabHovered;
    public Sprite TabSelected;

    public TabButton SelectedTab;

    public List<GameObject> TabContents;

    public void Subscribe(TabButton button)
    {
        if(TabButtons == null)
        {
            TabButtons = new List<TabButton>();
        }

        TabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(SelectedTab == null || button != SelectedTab)
        {
            button.Background.sprite = TabHovered;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if(SelectedTab != null)
        {
            SelectedTab.Deselect();
        }

        SelectedTab = button;

        SelectedTab.Select();

        ResetTabs();
        button.Background.sprite = TabSelected;

        var index = button.transform.GetSiblingIndex();
        for(int i = 0; i < TabContents.Count; i++)
        {
            if(i == index)
            {
                TabContents[i].SetActive(true);
            }
            else
            {
                TabContents[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (var button in TabButtons)
        {
            if (SelectedTab != null && button == SelectedTab) continue;

            button.Background.sprite = TabIdle;
        }
    }
}
