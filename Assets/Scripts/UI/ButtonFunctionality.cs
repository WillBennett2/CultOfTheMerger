using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject m_objectToToggle;
    [SerializeField] private GameObject m_objectToActivate;
    [SerializeField] private GameObject m_objectToDeactivate;

    public void ToggleUI()
    {
        bool uiActiveState = m_objectToToggle.activeSelf;
        m_objectToToggle.SetActive(!uiActiveState);
    }

    public void ChangeCurrentUI()
    {
        m_objectToActivate.SetActive(true);
        m_objectToDeactivate.SetActive(false);
    }
}
