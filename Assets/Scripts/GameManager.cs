using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region  Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance)
            Destroy(gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(this);
        
    }
    #endregion
    
    [SerializeField] private GameObject m_selectedPawn;

    [SerializeField] private GridManager m_gridManager;
    [SerializeField] private List<Buildings> m_buildings;
    [SerializeField] private bool m_regen;
    public GameObject SelectedPawn
    {
        get { return m_selectedPawn; }

        set { m_selectedPawn = value; }

    }

    public List<Buildings> Buildings
    {
        get
        {
            return m_buildings;
        }
    }
    public void RegenGird()
    {
        m_regen = false;
        m_gridManager.RegenerateGrid(5,5);
        for (int i = 0; i < m_buildings.Count; i++)
        {
            m_buildings[i].PopulateGrid();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_gridManager = Camera.main.GetComponent<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_regen)
            RegenGird();
    }
}
