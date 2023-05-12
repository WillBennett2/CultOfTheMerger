using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private AudioManager m_AudioManager;
    private Camera m_mainCamera;
    private float m_startXPos;
    private float m_startZPos;

    private bool m_isDragging = false;
    private PawnMovement m_thisPawnMovementScript;
    private Buildings m_thisPawnBuildingScript;
    

    [SerializeField] private bool m_isDraggable = false;
    [SerializeField] private bool m_isInteractable = false;
    [SerializeField] private GameManager m_gameManager;
    private bool m_isMinion;
    
    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_AudioManager = m_gameManager.GetComponent<AudioManager>();
    }
    void Start()
    {
        m_thisPawnMovementScript = GetComponent<PawnMovement>();
        m_thisPawnBuildingScript = GetComponent<Buildings>();
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
        //HandleTouchDown(Input.mousePosition);
    }

    public void TouchPerformedInput(Vector3 position)
    {
        HandleTouchDown(position);   
    }
    public void HoldPerformedInput(Vector3 position)
    {
        HandleHoldInput(position);
    }

    private void HandleTouchDown(Vector3 mousePos)
    {
       
        if (m_isInteractable)
        {
            if (m_thisPawnBuildingScript != null)
            {
                m_thisPawnBuildingScript.Tapped();
            }
            
            m_gameManager.SelectedPawn = gameObject;
        }
    }
    private void HandleHoldInput(Vector3 mousePos)
    {

        if (!m_mainCamera.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = m_mainCamera.ScreenToWorldPoint(mousePos);

        m_startXPos = mousePos.x - transform.localPosition.x;
        m_startZPos = mousePos.z - transform.localPosition.z;
        m_isDragging = true;
        if (m_isDraggable)
        {
            if (m_thisPawnMovementScript != null)
            {
                m_thisPawnMovementScript.BeingHeld();
            }
        }
    }

    public void TouchCancelledInput()
    {
        HandleTouchUp();
    }
    private void OnMouseUp()
    {
    }
    private void HandleTouchUp()
    {
        if (m_isDraggable && m_isDragging)
        {
            m_isDragging = false;
            
            m_thisPawnMovementScript.Dropped();
            m_AudioManager.Play("DropPawn");
        }
    }

    private void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;;

        if(!m_mainCamera.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = m_mainCamera.ScreenToWorldPoint(mousePos);
        transform.localPosition = new Vector3(mousePos.x - m_startXPos,transform.position.y ,mousePos.z - m_startZPos);
    }
}

