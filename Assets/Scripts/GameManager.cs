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
    [SerializeField] private Transform m_alterTransform;
    private bool m_levelUp = false;
    [SerializeField]public bool m_levelUpBegin = false;
    [SerializeField] private GooglePlayManager m_googlePlayManager;

    [SerializeField] private bool m_Save;
    [SerializeField] private bool m_load = false;
    [SerializeField] private SaveManager m_saveManagerScript;
       
    [SerializeField] private Buildings m_undeadEnemyBuilding;
    [SerializeField] private int m_numOfUndeadSpawns;
    [SerializeField] private int m_maxOfUndeadSpawns;

    [SerializeField] private List<Buildings> m_spawnQueue;
    private bool m_hasSpawned;


    public int ID
    {
        get
        {
            m_idCount++;
            return m_idCount;
        }
    }
    public bool HasSpawned
    {
        get { return m_hasSpawned; }
        set { m_hasSpawned = value; }
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
    public int GetNumOfUndeadSpawned
    {
        get
        { return m_numOfUndeadSpawns; }
        set { m_numOfUndeadSpawns = value; }
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
    public void LevelUpCult()
    {
        if (!m_levelUp)
        {
            m_levelUpBegin = true;
            StartCoroutine(DelayGridRegen(0.5f));
            //set camera and alter at new position
            Camera.main.transform.position = new Vector3(1.1f, 13f, -0.5f);
            //move alter to new position
            m_alterTransform.position = new Vector3(1.27f, 0.63f, -6.74f);
            //give achievement
            m_googlePlayManager.AchievementsTest(GPGSIds.achievement_going_big);
            m_levelUp = true;
            m_levelUpBegin = false;
            SaveData();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gridManager = Camera.main.GetComponent<GridManager>();
        m_googlePlayManager = GetComponent<GooglePlayManager>();
        StartCoroutine(LateStart(1f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadData();
    }
    IEnumerator DelayGridRegen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RegenGird();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_regen)
            RegenGird();
        if(m_Save)
            SaveData();
        if (m_levelUpBegin) 
            LevelUpCult();
    }

    public void SaveData()
    {
        if(m_levelUpBegin)
        {
            return;
        }

        m_saveManagerScript.SavePawns(m_moveablePawns);
        
    }
    void LoadData()
    {
        m_saveManagerScript.LoadPawns();
        m_load = true;
    }
    public void MinionSpawned(PawnDefinitions.MMinionType minionType)
    {
        if(minionType == PawnDefinitions.MMinionType.Undead)
        {
            HandleUndeadEnemySpawn();
        }
    }
    void HandleUndeadEnemySpawn()
    {
        m_numOfUndeadSpawns++;
        if(m_numOfUndeadSpawns>=m_maxOfUndeadSpawns)
        {
            m_undeadEnemyBuilding.Tapped();
            m_numOfUndeadSpawns = 0;
        }
    }
    public void AddSelfToQueue(Buildings building)
    {
        m_spawnQueue.Add(building);
    }
    public List<Buildings> GetSpawnQueue() { return m_spawnQueue; }
    public void AttemptToFindFreeSpace()
    {
        if (m_spawnQueue.Count > 0)
        {
            m_spawnQueue[0].Tapped();
            if (m_hasSpawned)
            { 
                m_hasSpawned = false;
                m_spawnQueue.RemoveAt(0);
            }

        }
    }
}
