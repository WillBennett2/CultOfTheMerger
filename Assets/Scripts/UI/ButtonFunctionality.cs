using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour
{
    [SerializeField] private AudioManager m_audioManager;
    [SerializeField] private GameObject m_objectToToggle;
    [SerializeField] private GameObject m_objectToActivate;
    [SerializeField] private GameObject m_objectToDeactivate;

    public void ToggleUI()
    {
        m_audioManager.Play("ButtonSound");
        bool uiActiveState = m_objectToToggle.activeSelf;
        m_objectToToggle.SetActive(!uiActiveState);
    }

    public void ChangeCurrentUI()
    {
        m_audioManager.Play("ButtonClick");
        m_objectToActivate.SetActive(true);
        m_objectToDeactivate.SetActive(false);
    }
}
