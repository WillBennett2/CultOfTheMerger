using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    private PawnMerge m_pawnMergeScript;
    private PawnSacrifice m_pawnSacrificeScript;
    [SerializeField]private GameManager m_gameManager;
    [Header("Movement Data")]
    [SerializeField] private GameObject m_homeTile;
    [SerializeField] private int m_homeTileNum;
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private bool m_beingMoved = false;
    private Transform m_childTransform;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_GridManager = Camera.main.GetComponent<GridManager>();
        m_pawnMergeScript = GetComponent<PawnMerge>();
        m_pawnSacrificeScript = GetComponent<PawnSacrifice>();
        m_childTransform = GetComponentInChildren<Transform>();
    }
    private void Start()
    {

    }

    private void OnDestroy()
    {
        if(m_homeTileNum!=-1)
            m_GridManager.UpdateTile(m_homeTileNum,false);
        m_gameManager.Pawns.Remove(this);
        if (m_gameManager.IsLoaded&&m_homeTileNum==-1)
            m_gameManager.SaveData();
    }

    public bool GetBeingMoved()
    {
        return m_beingMoved;
    }

    public GameObject GetHomeTile
    {
        get
        {
            return m_homeTile;    
        }
        set
        {
            m_homeTile = value;
        }
        
    }

    public int GetHomeTileNum
    {
        get { return m_homeTileNum; }
        set { m_homeTileNum = value; }
    }
    public PawnMerge GetPawnMerge
    {
        get { return m_pawnMergeScript; }   
    }

    public void ClearHomeTile()
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
        m_pawnSacrificeScript.AttemptSacrifice();
        m_pawnMergeScript.AttemptDropMerge();
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
        if (m_gameManager.IsLoaded)
            m_gameManager.SaveData();
    }
 
}
