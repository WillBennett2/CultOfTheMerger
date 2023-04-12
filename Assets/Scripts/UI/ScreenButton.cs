using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ScreenButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public ScreenGroup m_screenGroup;
    public Image m_background;

    public void OnPointerClick(PointerEventData eventData)
    {
        m_screenGroup.OnScreenSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_screenGroup.OnScreenEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_screenGroup.OnScreenExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_background = GetComponent<Image>();
        m_screenGroup.Subscribe(this);
    }
    
}
