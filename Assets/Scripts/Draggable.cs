using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Camera myCam;
    
    private float startXPos;
    private float startZPos;

    private bool isDragging = false;
    [SerializeField]private bool isDragable = false;

    private void FixedUpdate()
    {
        if (isDragging&&isDragable)
        {
            DragObject();
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - transform.localPosition.x;
        startZPos = mousePos.z - transform.localPosition.z;

        isDragging = true;
        if (gameObject.GetComponent<Minions>()!=null)
        {
            gameObject.GetComponent<Minions>().BeingHeld();
        }
        else if(gameObject.GetComponent<Buildings>()!=null)
        {
            gameObject.GetComponent<Buildings>().Tapped();
        }

    }

    private void OnMouseUp()
    {
        isDragging = false;
        gameObject.GetComponent<Minions>().Dropped();
    }

    public void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;

        if(!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);
        transform.localPosition = new Vector3(mousePos.x - startXPos,transform.position.y ,mousePos.z - startZPos);
    }
}
