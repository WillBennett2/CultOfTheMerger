using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float m_xStartPos,m_yStartPos ,m_zStartPos;
    [SerializeField] private int m_columnLength, m_rowLength;
    [SerializeField] private float m_xDistance, m_zDistance;
    [SerializeField] private GameObject m_parentObject;
    [SerializeField] private GameObject m_gridPrefeb;

    [SerializeField] public GameObject[] m_grid;
    // Start is called before the first frame update
    void Start()
    {
        m_grid = new GameObject[m_columnLength * m_rowLength];
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RegenerateGrid()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        for (int i = 0; i < m_columnLength * m_rowLength; i++)
        {
            //GameObject tempObject= Instantiate(m_gridPrefeb, new Vector3(m_xStartPos + (m_xDistance* (i % m_columnLength)),m_yStartPos + 0.0f ,m_zStartPos - (m_zDistance * (i / m_columnLength))),Quaternion.identity); 
            
            
            m_grid[i] = Instantiate(m_gridPrefeb, new Vector3(m_xStartPos + (m_xDistance* (i % m_columnLength)),m_yStartPos + 0.0f ,m_zStartPos - (m_zDistance * (i / m_columnLength))),Quaternion.identity);
            m_grid[i].transform.parent = m_parentObject.transform;
        }
    }
    
    
}
