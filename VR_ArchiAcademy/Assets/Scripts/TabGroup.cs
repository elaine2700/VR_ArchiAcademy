using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    //Following tutorial
    //https://www.youtube.com/watch?v=211t6r12XPQ

    public List<TabButtons> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabSelected;
    public TabButtons selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButtons button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButtons>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButtons button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
        
    }

    public void OnTabExit(TabButtons button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButtons button)
    {
        Debug.Log("Tab selected");
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        selectedTab.Select();

        ResetTabs();
        button.background.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach(TabButtons button in tabButtons)
        {
            if(selectedTab != null && selectedTab == button)
                continue;
            button.background.color = tabIdle;
        }
    }

}
