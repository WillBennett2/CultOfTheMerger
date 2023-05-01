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
    [SerializeField] private List<PawnMovement> m_moveablePawns;
    [SerializeField] private bool m_regen;
    [SerializeField] private int m_idCount;

    [SerializeField] private bool m_Save;
    [SerializeField] private bool m_load = false;
    [SerializeField] private SaveManager m_saveManagerScript;

    public int ID
    {
        get
        {
            m_idCount++;
            return m_idCount;
        }
    }
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
    public List<PawnMovement> Pawns
    {
        get
        {
            return m_moveablePawns;
        }
    }

    public bool IsLoaded
    {
        get
        { return m_load; }
        set
        {
            m_load = value;
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
        StartCoroutine(LateStart(1f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadData();
    }


    // Update is called once per frame
    void Update()
    {
        if (m_regen)
            RegenGird();
        if(m_Save)
            SaveData();

    }

    public void SaveData()
    {
        m_saveManagerScript.SavePawns(m_moveablePawns);
    }
    void LoadData()
    {
        m_saveManagerScript.LoadPawns();
        m_load = true;
    }
}
