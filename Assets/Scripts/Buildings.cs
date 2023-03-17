using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

struct MSpawnType
{
    public enum BuildingType {Grave,Slime,PLACEHOLDER}
}


public class Buildings : MonoBehaviour
{
    [SerializeField] private PawnDefinitions.MPawnObjects m_objectType;
    [SerializeField] private PawnDefinitions.MMinionType m_minionType;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private PawnDefinitions.MBuildingType m_buildingType;
    [SerializeField] private PawnDefinitions.MItemType m_itemType;
    [SerializeField] private BuildingItems m_buildingItems ;
    [SerializeField] private GameObject m_minionPrefab;
    private GameObject m_pawnObject;

    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private GameObject[] m_gridRef;
    private int m_sizeOfGrid;

    [SerializeField] private ObjectData m_objectDataRef;
    [SerializeField] private ObjectData.Minion[] m_objectDatas;
    [SerializeField] private int m_minionTypesNum = 1;
    [SerializeField] private ObjectData.Minion[] m_minionData;
    
    // Start is called before the first frame update
    void Start()
    {
        //auto assign item based on type?
        m_GridManager = Camera.main.GetComponent<GridManager>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_gameManager.Buildings.Add(this);
        AssignObjectData();
        StartCoroutine(LateStart(1));
        
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PopulateGrid();
    }

    private void AssignObjectData()
    {
        m_minionData = new ObjectData.Minion[m_minionTypesNum];
        m_objectDatas = m_objectDataRef.Objects;
        for (int i = 0; i < m_minionTypesNum; i++)
        {
            m_minionData[i] = m_objectDatas[i];
        }
    }
    private void OnDestroy()
    {
        m_gameManager.Buildings.Remove(this);
    }

    public void PopulateGrid()
    {
        m_sizeOfGrid = m_GridManager.m_grid.Length;
        m_gridRef = new GameObject[m_sizeOfGrid];
        for (int i = 0; i < m_sizeOfGrid; i++)
        {
            m_gridRef[i] = m_GridManager.m_grid[i];
        }
    }

    public void Tapped()
    {
        if(gameObject.GetComponent<Interactable>().TappedObject == gameObject)
            SpawnPawn();

    }
    private int GenRandomNum()
    {
        return (Random.Range(0,2 ));
    }

    private int FindFreeTile()
    {
        for (int i = 0; i < m_sizeOfGrid; i++)
        {
            if (!m_gridRef[i].GetComponent<TileInfo>().m_tileTaken)
            {
                return i;
            }
        }

        return -1;
    }
    private void SpawnPawn()
    {
        int tileNum = FindFreeTile();
        if (tileNum != -1)
        {
            m_pawnObject = Instantiate(m_minionPrefab,
                new Vector3(m_gridRef[tileNum].transform.position.x, m_minionPrefab.transform.position.y,
                    m_gridRef[tileNum].transform.position.z), Quaternion.identity);

            m_GridManager.UpdateTile(tileNum,true);
            if (m_objectType == PawnDefinitions.MPawnObjects.Building)
            {
                SetBuildingType();
            }

            if (m_objectType == PawnDefinitions.MPawnObjects.Minions)
            {
                SetMinionType();
            }

            if (m_objectType == PawnDefinitions.MPawnObjects.Item)
            {
                SetItemType();
            }
            m_pawnObject.GetComponent<PawnMovement>().SetHomeTile(m_gridRef[tileNum],tileNum);
            m_gameManager.Pawns.Add(m_pawnObject.GetComponent<PawnMovement>());
            m_pawnObject = null;
        }
    }
    private void SetBuildingType()
    {
        PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
        if (pawnMergeScript)
        {
            if (GenRandomNum() == 0)
            {
                m_buildingType = PawnDefinitions.MBuildingType.Grave;
            }
            else
            {
                m_buildingType = PawnDefinitions.MBuildingType.Grave;
            }

            if (m_buildingType == PawnDefinitions.MBuildingType.Grave)
            {
                //object type,minion type, mana type, building type, item type, pawn level, current level
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType,
                    m_buildingItems.GetPawnLevels(0), 0);
            }
            else
            {
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType,
                    m_buildingItems.GetPawnLevels(1), 0);
            }
        }
    }
    private void SetItemType()
    {
        PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
        if (pawnMergeScript)
        {
            if (GenRandomNum() == 0)
            {
                m_itemType = PawnDefinitions.MItemType.Coin;
            }
            else
            {
                m_itemType = PawnDefinitions.MItemType.Coin;
            }

            if (m_itemType == PawnDefinitions.MItemType.Coin)
            {
                //object type,minion type, mana type, building type, item type, pawn level, current level
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType,
                    m_buildingItems.GetPawnLevels(0), 0);
            }
            else
            {
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType,
                    m_buildingItems.GetPawnLevels(1), 0);
            }
        }
    }
    private void SetMinionType()
    {
        m_pawnObject.GetComponent<Minions>().ID = m_gameManager.ID;
        PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
        PawnMana manaData = m_pawnObject.GetComponent<PawnMana>();
        if (pawnMergeScript)
        {
            if (GenRandomNum() == 0)
            {
                m_minionType = m_minionData[0].m_minionType;
                pawnMergeScript.SetPawnValues(m_objectType,m_minionType, m_minionData[0].m_manaType,m_buildingType, m_itemType, m_minionData[0].m_pawnLevels, 0);
                manaData.SetManaValues(m_minionData[0].m_manaType, m_objectDatas[0].m_baseMana,m_objectDatas[0].m_manaMultiplier); 
            }
            else
            {
                m_minionType = m_minionData[1].m_minionType;
                manaData.SetManaValues(m_minionData[1].m_manaType, m_objectDatas[1].m_baseMana,m_objectDatas[1].m_manaMultiplier); 
                pawnMergeScript.SetPawnValues(m_objectType,m_minionType, m_minionData[1].m_manaType,m_buildingType, m_itemType, m_minionData[1].m_pawnLevels, 0);
            }
        }
    }
}
