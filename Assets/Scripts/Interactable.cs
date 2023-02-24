using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Camera m_mainCamera;
    private float m_startXPos;
    private float m_startZPos;

    private bool m_isDragging = false;

    [SerializeField] private bool m_isDraggable = false;
    [SerializeField] private bool m_isInteractable = false;
    [SerializeField] private GameManager m_gameManager;
    private bool m_isMinion;
    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_isDragging&&m_isDraggable)
        {
            DragObject();
        }
    }

    public GameObject TappedObject
    {
        get
        {
            return m_gameManager.SelectedPawn;

        }
    }
    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!m_mainCamera.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = m_mainCamera.ScreenToWorldPoint(mousePos);

        m_startXPos = mousePos.x - transform.localPosition.x;
        m_startZPos = mousePos.z - transform.localPosition.z;
        
        m_isDragging = true;
        if(m_isDraggable)
        {
            if (gameObject.GetComponent<PawnMovement>() != null)
            {
                gameObject.GetComponent<PawnMovement>().BeingHeld();
            }
        }

        if (m_isInteractable)
        {
            if (gameObject.GetComponent<Buildings>() != null)
            {
                gameObject.GetComponent<Buildings>().Tapped();
            }
        }
        m_gameManager.SelectedPawn = gameObject;
    }

    private void OnMouseUp()
    {
        if (m_isDraggable)
        {
            m_isDragging = false;
            
            gameObject.GetComponent<PawnMovement>().Dropped();
        }
    }
    private void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;

        if(!m_mainCamera.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = m_mainCamera.ScreenToWorldPoint(mousePos);
        transform.localPosition = new Vector3(mousePos.x - m_startXPos,transform.position.y ,mousePos.z - m_startZPos);
    }
}

