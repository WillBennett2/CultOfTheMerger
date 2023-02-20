using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private string m_draggingTag;
    [SerializeField] private Camera m_camera;

    private Vector3 m_distance;
    private float m_xPos;
    private float m_yPos;

    private bool m_touched = false;
    private bool m_dragging = false;

    private Transform m_toDrag;
    private Rigidbody m_rigidBodyToDrag;
    private Vector3 m_previousPos;
    
    void FixedUpdate()
    {
        if (Input.touchCount !=1)
        {
            m_dragging = false;
            m_touched = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 touchPos = touch.position;
        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = m_camera.ScreenPointToRay(touchPos);
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(m_draggingTag))
            {
                m_toDrag = hit.transform;
                m_previousPos = m_toDrag.position;
                m_rigidBodyToDrag = m_toDrag.GetComponent<Rigidbody>();

                m_distance = m_camera.WorldToScreenPoint(m_previousPos);
                m_xPos = Input.GetTouch(0).position.x - m_distance.x;
                m_yPos = Input.GetTouch(0).position.y - m_distance.y;
                SetDraggingProp(m_rigidBodyToDrag);
                m_touched = true;

            }

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Building"))
            {
                hit.transform.GetComponent<Buildings>().Tapped();
            }
        }

        if (m_touched== true && touch.phase == TouchPhase.Moved)
        {
            m_dragging = true;

            float posXNow = Input.GetTouch(0).position.x - m_xPos;
            float posYNow = Input.GetTouch(0).position.y - m_yPos;
            Vector3 fingerPos = new Vector3(posXNow, posYNow, m_distance.z);

            Vector3 worldPos = m_camera.ScreenToWorldPoint(fingerPos) - m_previousPos;
            worldPos = new Vector3(worldPos.x, 0.0f, worldPos.z);

            m_rigidBodyToDrag.velocity = worldPos / (Time.deltaTime * 10);

            m_previousPos = m_toDrag.position;
        }

        if (m_dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            m_dragging = false;
            m_touched = false;
            m_previousPos = new Vector3(0.0f, 0.0f, 0.0f);
            SetFreeProp(m_rigidBodyToDrag);
        }
    }

    private void SetDraggingProp(Rigidbody rb)
    {
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.drag = 20;
    }
    private void SetFreeProp(Rigidbody rb)
    {
        rb.isKinematic = true;
        rb.useGravity = true;
        rb.drag = 5;
    }
}
