using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private float m_xStartPos,m_yStartPos ,m_zStartPos;
    [SerializeField] private int m_columnLength, m_rowLength;
    [SerializeField] private float m_xDistance, m_zDistance;
    [SerializeField] private GameObject m_parentObject;
    [SerializeField] private GameObject m_gridPrefeb;
    [SerializeField] private bool m_regen;


    [SerializeField] public GameObject[] m_grid;
    private bool m_closingGame = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_grid = new GameObject[m_columnLength * m_rowLength];
        CreateGrid();
    }
    private void OnDestroy()
    {
        m_closingGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_regen)
        {
            RegenerateGrid(4,4);
            m_regen = false;
        }
    }

    void ClearGrid()
    {
        for (int i = 0; i<m_grid.Length; i++)
        {
            Destroy(m_grid[i]);
        }
    }
    public void RegenerateGrid(int columnLength, int rowLength)
    {
        ClearGrid();
        m_columnLength = columnLength;
        m_rowLength = rowLength;
        m_grid = new GameObject[m_columnLength * m_rowLength];
        CreateGrid();
        for (int i = 0; i < m_gameManager.Pawns.Count; i++)
        {
            m_gameManager.Pawns[i].Dropped();
        }
    }
    void CreateGrid()
    {
        for (int i = 0; i < m_columnLength * m_rowLength; i++)
        {
            m_grid[i] = Instantiate(m_gridPrefeb, new Vector3(m_xStartPos + (m_xDistance* (i % m_columnLength)),m_yStartPos + 0.0f ,m_zStartPos - (m_zDistance * (i / m_columnLength))),Quaternion.identity);
            m_grid[i].transform.parent = m_parentObject.transform;
            m_grid[i].GetComponent<TileInfo>().m_tileNum = i;
        }
    }
    
    public void UpdateTile(int tileNum, bool state)
    {
        if (m_closingGame)
            return;

        if (tileNum < m_columnLength * m_rowLength)
        {
            if(m_grid[tileNum].GetComponent<TileInfo>())
                m_grid[tileNum].GetComponent<TileInfo>().m_tileTaken = state;
        }
    }
    
    
}
