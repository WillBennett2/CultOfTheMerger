using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGroup : MonoBehaviour
{
    public List<ScreenButton> m_screenButtons;
    public List<GameObject> m_screens;

    public void Subscribe(ScreenButton button)
    {
        if(m_screenButtons == null)
        {
            m_screenButtons = new List<ScreenButton>();
        }
        m_screenButtons.Add(button);
    }
    public void OnScreenEnter(ScreenButton button)
    {
    
    }
    public void OnScreenExit(ScreenButton button)
    {
    
    }
    public void OnScreenSelected(ScreenButton button)
    {
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < m_screens.Count; i++)
        {
            if(i == index)
            {
                m_screens[i].SetActive(true);
            }
            else
            {
                m_screens[i].SetActive(false);
            }
        }
    }
    
}
