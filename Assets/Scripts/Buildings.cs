using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Buildings : MonoBehaviour
{
    [SerializeField] private PawnDefinitions.MPawnObjects m_objectType;
    [SerializeField] private PawnDefinitions.MMinionType m_minionType;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private PawnDefinitions.MBuildingType m_buildingType;
    [SerializeField] private PawnDefinitions.MItemType m_itemType;
    [SerializeField] private PawnDefinitions.MSacrificeTypes m_sacrificeTypes;
    [SerializeField] private BuildingItems m_buildingItems ;
    [SerializeField] private GameObject m_minionPrefab;
    private GameObject m_pawnObject;
    [SerializeField] private int m_buildingLevel;
    [SerializeField] private int[] m_spawnCosts;
    [SerializeField] private BuildingData m_buildingData;


    private GameManager m_gameManager;
    private Inventory m_inventory;
    private GridManager m_GridManager;
    private GameObject[] m_gridRef;
    private int m_sizeOfGrid;

    [SerializeField] private ObjectData m_objectDataRef;
    private int m_minionTypesCount;
    [SerializeField] private List<ObjectData.Minion> m_minionData;
    private int m_itemTypesCount;
    [SerializeField] private List<ObjectData.Item> m_itemData;

    // Start is called before the first frame update
    void Start()
    {
        //auto assign item based on type?
        m_GridManager = Camera.main.GetComponent<GridManager>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_inventory = FindObjectOfType<Inventory>();
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
        m_minionTypesCount = m_objectDataRef.Minions.Length;
        for (int i = 0; i < m_minionTypesCount; i++)
        {
            m_minionData.Add(new ObjectData.Minion());
            m_minionData[i] = m_objectDataRef.Minions[i];
        }
        m_itemTypesCount = m_objectDataRef.Items.Length;
        for (int i = 0; i < m_itemTypesCount; i++)
        {
            m_itemData.Add(new ObjectData.Item());
            m_itemData[i] = m_objectDataRef.Items[i];
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
        else
        {
            SpawnPawn();
        }

    }
    private int GenRandomNum(int endNum)
    {
        return (Random.Range(0,endNum ));
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

    private void CreateObject(int tileNum)
    {
        m_pawnObject = Instantiate(m_minionPrefab,
            new Vector3(m_gridRef[tileNum].transform.position.x, m_minionPrefab.transform.position.y,
                m_gridRef[tileNum].transform.position.z), Quaternion.identity);
        m_pawnObject.GetComponent<PawnMovement>().SetHomeTile(m_gridRef[tileNum],tileNum);
        m_gameManager.Pawns.Add(m_pawnObject.GetComponent<PawnMovement>());
    }
    private void SpawnPawn()
    {
        ResetTypes();
        
        int tileNum = FindFreeTile();
        if (tileNum == -1)
        {
            return;
        }
        

        if (m_objectType == PawnDefinitions.MPawnObjects.Building)
        {
            SetBuildingType(tileNum);
            //m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues();
        }

        if (m_objectType == PawnDefinitions.MPawnObjects.Minions)
        {
            int randNum = GenRandomNum(m_minionTypesCount); 
            if(SetMinionType(tileNum,randNum))
                m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_minionData[randNum].m_sacrificialBaseValue,m_minionData[randNum].m_sacrificeType,m_minionData[randNum].m_sacrificialMultiplier);
        }

        if (m_objectType == PawnDefinitions.MPawnObjects.Item)
        {
            int randNum = GenRandomNum(m_itemTypesCount); 
            CreateObject(tileNum);
            SetItemType(randNum);
        }

        if (m_pawnObject != null)
        {
            m_GridManager.UpdateTile(tileNum,true);
            m_pawnObject = null;
        }
    
    }


    private void SetBuildingType(int tileNum)
    {
        int randNum = GenRandomNum(0);

        if (randNum == 0 && m_inventory.Rune.m_deathRuneCount >= m_buildingData.RuneCost.m_graveRuneCost )
        {
            m_buildingType = PawnDefinitions.MBuildingType.Grave;
            m_inventory.DeathRune = - m_buildingData.RuneCost.m_graveRuneCost;
        }
        else if (randNum == 1)
        {
            m_buildingType = PawnDefinitions.MBuildingType.Life;
        }

        //if xyz
        if (m_buildingType != PawnDefinitions.MBuildingType.Empty)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
            if (pawnMergeScript)
            {

                if (m_buildingType == PawnDefinitions.MBuildingType.Grave)
                {
                    //object type,minion type, mana type, building type, item type, pawn level, current level
                    pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType,m_sacrificeTypes,
                        m_buildingItems.GetPawnLevels(0), 0);
                }
                else
                {
                    pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType,m_sacrificeTypes,
                        m_buildingItems.GetPawnLevels(1), 0);
                }
            }
        }
    }
    private void SetItemType(int randNum)
    {
        PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
        if (pawnMergeScript)
        {
            pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType,  m_itemData[randNum].m_itemType,m_itemData[randNum].m_sacrificeType,
                    m_itemData[randNum].m_pawnLevels, 0);
            m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_itemData[randNum].m_sacrificialBaseValue,m_itemData[randNum].m_sacrificeType,m_itemData[randNum].m_sacrificialMultiplier);
        }
    }
    private bool SetMinionType(int tileNum,int randNum)
    {
        //check if enough mana to spawn
        if (m_minionData[randNum].m_manaType == PawnDefinitions.MManaType.Necro && m_inventory.NecroStore >= m_spawnCosts[m_buildingLevel])
        {
            m_inventory.NecroStore = -m_spawnCosts[m_buildingLevel];
            CreateObject(tileNum);
            m_pawnObject.GetComponent<Minions>().ID = m_gameManager.ID;
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
            PawnMana manaData = m_pawnObject.GetComponent<PawnMana>();
            if (pawnMergeScript)
            {
                m_minionType = m_minionData[randNum].m_minionType;
                pawnMergeScript.SetPawnValues(m_objectType,m_minionType, m_minionData[randNum].m_manaType,m_buildingType, m_itemType,m_minionData[randNum].m_sacrificeType, m_minionData[randNum].m_pawnLevels, 0);
                manaData.SetManaValues(m_minionData[randNum].m_manaType, m_minionData[randNum].m_baseMana,m_minionData[randNum].m_manaMultiplier);
            }

            return true;
        }
        return false;
    }

    private void ResetTypes()
    {
        m_minionType = PawnDefinitions.MMinionType.Empty;
        m_itemType = PawnDefinitions.MItemType.Empty;
        m_buildingType = PawnDefinitions.MBuildingType.Empty;
    }
}
