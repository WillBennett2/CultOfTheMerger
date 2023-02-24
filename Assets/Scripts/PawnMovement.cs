using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{

    [Header("Movement Data")]
    [SerializeField] private GameObject m_homeTile;
    [SerializeField] private int m_homeTileNum;
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private bool m_beingMoved = false;

    private void Start()
    {
        m_GridManager = Camera.main.GetComponent<GridManager>();
    }

    public bool GetBeingMoved()
    {
        return m_beingMoved;
    }

    public GameObject GetHomeTile()
    {
        return m_homeTile;
    }

    public int GetHomeTileNum()
    {
        return m_homeTileNum;
    }

    public void SetHomeTile()
    {
        m_GridManager.UpdateTile(m_homeTileNum,false);
    }

    public void BeingHeld()
    {
        m_beingMoved = true;
    }
    public void Dropped()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-Vector3.up, out hit))
        {
            if (hit.transform.GetComponent<TileInfo>()!= null)        //is tile below open
            {
                if (!hit.transform.GetComponent<TileInfo>().m_tileTaken) // take tile
                {
                    TileInfo hitTileInfo = hit.transform.GetComponent<TileInfo>();
                    m_GridManager.UpdateTile(m_homeTileNum,false);
                    GameObject newHomeTile = hit.transform.gameObject;
                    int newHomeTileNum = hitTileInfo.m_tileNum;
                    SetHomeTile(newHomeTile,newHomeTileNum );
                    m_GridManager.UpdateTile(m_homeTileNum,true);
                }
            }
        }
        gameObject.GetComponent<PawnMerge>().AttemptDropMerge();
        MoveToHomeTile();
        //Move current grid
        m_beingMoved = false;
    }
    public void SetHomeTile(GameObject newHomeTile,int newHomeTileNum)
    {
        m_homeTile = newHomeTile;
        m_homeTileNum = newHomeTileNum;
        MoveToHomeTile();
    }

    public void MoveToHomeTile()
    {
        transform.position = new Vector3(m_homeTile.transform.position.x,transform.position.y,m_homeTile.transform.position.z); //snapping back to grid
    }
 
}
