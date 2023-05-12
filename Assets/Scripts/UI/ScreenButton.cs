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
    [SerializeField] bool m_enabled = true;
    [SerializeField] private AudioManager m_audioManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(m_screenGroup!=null)
            m_screenGroup.OnScreenSelected(this);
        m_audioManager.Play("ButtonClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_screenGroup != null)
            m_screenGroup.OnScreenEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_screenGroup != null)
            m_screenGroup.OnScreenExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_background = GetComponent<Image>();
        if(m_enabled)
            m_screenGroup.Subscribe(this);
    }
    
}
