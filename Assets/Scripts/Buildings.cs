using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Buildings : MonoBehaviour
{
    public int BuildingUses
    {
        get
        {
            return m_maxUses;
        }
        set
        {
            m_maxUses = value;
        }
    }
    public bool IsBuildingLimited
    {
        get
        {
            return m_spawnsLimited;
        }
        set
        {
            m_spawnsLimited = value;
        }
    }
    [SerializeField] private PawnDefinitions.MPawnObjects m_objectType;
    [SerializeField] private PawnDefinitions.MMinionType m_minionType;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private PawnDefinitions.MBuildingType m_buildingType;
    [SerializeField] private PawnDefinitions.MItemType m_itemType;
    [SerializeField] private PawnDefinitions.MSacrificeTypes m_sacrificeTypes;
    [SerializeField] private PawnDefinitions.MEnemyTypes m_enemyTypes;
    [SerializeField] private PawnDefinitions.MRewardType m_rewardType;
    [SerializeField] private PawnDefinitions.MStoreType m_storeType;
    [SerializeField] private BuildingItems m_buildingItems;
    [SerializeField] private GameObject m_minionPrefab;
    private GameObject m_pawnObject;
    [SerializeField] private int m_buildingLevel;
    [SerializeField] private int[] m_spawnCosts;
    [SerializeField] private SpawnChances m_spawnChances;
    [SerializeField] private BuildingData m_buildingData;

    [Header("Spawn Uses")]
    [SerializeField] private bool m_spawnsLimited = false;
    [SerializeField] private int m_maxUses = 1;
    [SerializeField] private int m_limitedUsesCount;

    private GameManager m_gameManager;
    private Inventory m_inventory;
    private GridManager m_GridManager;
    [SerializeField] private GameObject[] m_gridRef;
    private int m_sizeOfGrid;

    [SerializeField] private ObjectData m_objectDataRef;
    [SerializeField] private ItemData m_itemDataRef;
    private int m_minionTypesCount;
    [SerializeField] private List<ObjectData.Minion> m_minionData;

    private int m_itemTypesCount;
    [SerializeField] private List<ItemData.Item> m_itemData;
    private int m_enemyTypesCount;
    [SerializeField] private List<ObjectData.Enemy> m_enemyData;
    private int m_rewardTypesCount;
    [SerializeField] private List<ObjectData.Reward> m_rewardData;
    private int m_storeTypesCount;
    [SerializeField] private List<ObjectData.Store> m_storeData;

    private string m_id;
    // Start is called before the first frame update
    private void Awake()
    {
        m_GridManager = Camera.main.GetComponent<GridManager>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_inventory = FindObjectOfType<Inventory>();
        //auto assign item based on type?
        m_gameManager.Buildings.Add(this);
        AssignObjectData();
        StartCoroutine(LateStart(1));


    }
    void Start()
    {
        GameEvents.m_current.onPawnLevelUp += LevelUpBuildingLevel;
        m_limitedUsesCount = m_maxUses;
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_id = GetComponent<ID>().GetID;
        PopulateGrid();
    }
    IEnumerator DelayTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    private void AssignObjectData()
    {
        m_minionTypesCount = m_objectDataRef.Minions.Length;
        for (int i = 0; i < m_minionTypesCount; i++)
        {
            m_minionData.Add(new ObjectData.Minion());
            m_minionData[i] = m_objectDataRef.Minions[i];
        }

        m_itemTypesCount = m_itemDataRef.Items.Length;
        for (int i = 0; i < m_itemTypesCount; i++)
        {
            m_itemData.Add(new ItemData.Item());
            m_itemData[i] = m_itemDataRef.Items[i];
        }
        m_enemyTypesCount = m_objectDataRef.Enemies.Length;
        for (int i = 0; i < m_enemyTypesCount; i++)
        {
            m_enemyData.Add(new ObjectData.Enemy());
            m_enemyData[i] = m_objectDataRef.Enemies[i];
        }
        m_rewardTypesCount = m_objectDataRef.Rewards.Length;
        for (int i = 0; i < m_rewardTypesCount; i++)
        {
            m_rewardData.Add(new ObjectData.Reward());
            m_rewardData[i] = m_objectDataRef.Rewards[i];
        }
        m_storeTypesCount = m_objectDataRef.Stores.Length;
        for (int i = 0; i < m_storeTypesCount; i++)
        {
            m_storeData.Add(new ObjectData.Store());
            m_storeData[i] = m_objectDataRef.Stores[i];
        }
    }
    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpBuildingLevel;
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
        int tileNum = FindFreeTile();

        if (tileNum != -1 && GetComponent<Interactable>() != null)
        {
            if (m_spawnsLimited)
            {
                m_limitedUsesCount--;
            }

            if (m_limitedUsesCount <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (tileNum != -1)
        {
            SpawnPawn(true, tileNum, 0, 0, "", "", "", "", "");
        }

    }
    public void LoadPawn(int tileNum, int pawnIndex, int pawnLevel, string minionType, string itemType, string rewardType, string enemyType, string storeType)
    {
        SpawnPawn(false, tileNum, pawnIndex, pawnLevel, minionType, itemType, rewardType, enemyType, storeType);
    }
    private int GenRandomNum(int endNum)
    {
        return (Random.Range(0, endNum));
    }
    private void SpawnPawn(bool newPawn, int tileNum, int pawnIndex, int pawnLevel, string minionType, string itemType, string rewardType, string enemyType, string storeType)
    {
        ResetTypes();

        if (m_objectType == PawnDefinitions.MPawnObjects.Building)
        {
            SetBuildingType(newPawn, tileNum, pawnLevel, minionType);
            LevelUpPawn(pawnLevel);
        }

        if (m_objectType == PawnDefinitions.MPawnObjects.Minions)
        {
            int minionChance = GenRandomNum(100);
            int levelToSpawn = 0;
            string minionNameToSpawn = null;
            int minionIndex = -1;
            foreach (SpawnChances.MinionPartsChances chance in m_spawnChances.Chances[m_buildingLevel].m_chances)
            {
                if (chance.m_chance >= minionChance)
                {
                    levelToSpawn = chance.m_minionLevel;//pass this to set minion type
                    minionNameToSpawn = chance.m_minionName;
                    break;
                }
            }
            for (int i = 0; i < m_minionTypesCount; i++)
            {
                if (minionNameToSpawn != null && m_minionData[i].m_name == minionNameToSpawn)
                {
                    minionIndex = i;
                }
            }
            if (!newPawn)
            {
                levelToSpawn = pawnLevel;
                minionIndex = pawnIndex;
            }
            //int randNum = GenRandomNum(m_minionTypesCount); 
            if (minionIndex != -1 && SetMinionType(newPawn, tileNum, minionIndex, levelToSpawn))
                m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_minionData[minionIndex].m_sacrificialBaseValue, m_minionData[minionIndex].m_sacrificeType, m_minionData[minionIndex].m_sacrificialMultiplier);
            //StartCoroutine(DelayTime(2f));
            LevelUpPawn(levelToSpawn);
        }

        if (m_objectType == PawnDefinitions.MPawnObjects.Item)
        {
            int itemChance = GenRandomNum(100);
            int levelToSpawn = 0;
            string itemNameToSpawn = null;
            int itemIndex = -1;
            foreach (SpawnChances.MinionPartsChances chance in m_spawnChances.Chances[m_buildingLevel].m_chances)
            {
                if (chance.m_chance >= itemChance)
                {
                    levelToSpawn = chance.m_minionLevel;//pass this to set minion type
                    itemNameToSpawn = chance.m_minionName;
                    break;
                }
            }
            Debug.Log(itemNameToSpawn);
            for (int i = 0; i < m_itemTypesCount; i++)
            {
                if (itemNameToSpawn != null && m_itemData[i].m_name == itemNameToSpawn)
                {
                    itemIndex = i;
                }
            }
            if (!newPawn)
            {
                levelToSpawn = pawnLevel;
                itemIndex = pawnIndex;
            }

            //int randNum = GenRandomNum(m_minionTypesCount);)
            SetItemType(newPawn, tileNum, itemIndex, levelToSpawn);
            LevelUpPawn(levelToSpawn);
        }
        if (m_objectType == PawnDefinitions.MPawnObjects.Enemy)
        {
            SetEnemyType(newPawn, tileNum);
        }
        if (m_objectType == PawnDefinitions.MPawnObjects.Reward)
        {
            int randNum = GenRandomNum(m_rewardTypesCount);
            SetRewardType(newPawn, tileNum);
        }
        if (m_objectType == PawnDefinitions.MPawnObjects.Store)
        {
            SetStoreType(newPawn, tileNum, storeType);
            int levelToSpawn = 0;
            if (!newPawn)
            {
                levelToSpawn = pawnLevel;
            }
            LevelUpPawn(levelToSpawn);
        }


        if (m_pawnObject != null)
        {
            m_gameManager.Pawns.Add(m_pawnObject.GetComponent<PawnMovement>());
            m_pawnObject.GetComponent<PawnMovement>().SetHomeTile(m_gridRef[tileNum], tileNum);
            m_GridManager.UpdateTile(tileNum, true);
            if (m_objectType == PawnDefinitions.MPawnObjects.Minions)
            {
                m_gameManager.MinionSpawned(m_minionType);
            }
            m_pawnObject = null;
        }

    }
    private void CreateObject(int tileNum)
    {
        m_pawnObject = Instantiate(m_minionPrefab,
            new Vector3(m_gridRef[tileNum].transform.position.x, m_minionPrefab.transform.position.y,
                m_gridRef[tileNum].transform.position.z), Quaternion.identity);
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

    private void SetBuildingType(bool newPawn, int tileNum, int levelToSpawn, string minionType)
    {

        if (!newPawn)
        {
            if (minionType == "Undead")
            {
                m_minionType = PawnDefinitions.MMinionType.Undead;
                m_buildingType = PawnDefinitions.MBuildingType.Grave;
            }
            else if (minionType == "Plant")
            {
                m_minionType = PawnDefinitions.MMinionType.Plant;
                m_buildingType = PawnDefinitions.MBuildingType.Life;
            }
            else if (minionType == "Demon")
            {
                m_minionType = PawnDefinitions.MMinionType.Demon;
                m_buildingType = PawnDefinitions.MBuildingType.Hell;
            }
        }
        else
        {
            if (!Costs())
            {
                return;
            }
            if (m_minionType == PawnDefinitions.MMinionType.Undead)
            {
                m_buildingType = PawnDefinitions.MBuildingType.Grave;
            }
            else if (m_minionType == PawnDefinitions.MMinionType.Plant)
            {
                m_buildingType = PawnDefinitions.MBuildingType.Life;
            }
            else if (m_minionType == PawnDefinitions.MMinionType.Demon)
            {
                m_buildingType = PawnDefinitions.MBuildingType.Hell;
            }
        }

        //if xyz
        if (m_buildingType != PawnDefinitions.MBuildingType.Empty)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();

            if (pawnMergeScript)
            {
                //object type,minion type, mana type, building type, item type, pawn level, current level
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemType, m_sacrificeTypes, m_enemyTypes, m_rewardType, m_storeType
                    , m_buildingItems.GetPawnLevels(0), levelToSpawn);
            }
        }
    }
    private void SetItemType(bool newPawn, int tileNum, int itemIndex, int levelToSpawn)
    {
        bool canSpawn = false;
        if (!newPawn)
        {
            canSpawn = true;
        }
        else
        {
            if (m_itemType == PawnDefinitions.MItemType.DeathRune)
            {
                canSpawn = true;
            }
            else if (m_itemType == PawnDefinitions.MItemType.LifeRune)
            {
                canSpawn = true;
            }
            else if (m_itemType == PawnDefinitions.MItemType.HellRune)
            {
                canSpawn = true;
            }
            else if (m_itemType == PawnDefinitions.MItemType.Coin)
            {
                canSpawn = true;
            }
            else if (m_itemType == PawnDefinitions.MItemType.Gem)
            {
                canSpawn = true;
            }
        }
        if (canSpawn == true)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
            if (pawnMergeScript && itemIndex != -1)
            {
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType, m_itemData[itemIndex].m_itemType, m_itemData[itemIndex].m_sacrificeType, m_enemyTypes, m_rewardType, m_storeType,
                        m_itemData[itemIndex].m_pawnLevels, levelToSpawn);
                m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_itemData[itemIndex].m_sacrificialBaseValue, m_itemData[itemIndex].m_sacrificeType, m_itemData[itemIndex].m_sacrificialMultiplier);
                pawnMergeScript.GetPawnDataIndex = itemIndex;
            }
        }
    }
    private bool SetMinionType(bool newPawn, int tileNum, int minionIndex, int levelToSpawn)
    {
        bool canSpawn = false;
        //check if enough mana to spawn
        if (!newPawn) { canSpawn = true; }
        else
        {
            if (m_minionData[minionIndex].m_manaType == PawnDefinitions.MManaType.Necro && m_inventory.NecroStore >= m_spawnCosts[m_buildingLevel])
            {
                canSpawn = true;
                m_inventory.NecroStore -= m_spawnCosts[m_buildingLevel];
            }
            if (m_minionData[minionIndex].m_manaType == PawnDefinitions.MManaType.Life && m_inventory.LifeStore >= m_spawnCosts[m_buildingLevel])
            {
                canSpawn = true;
                m_inventory.LifeStore -= m_spawnCosts[m_buildingLevel];
            }
            if (m_minionData[minionIndex].m_manaType == PawnDefinitions.MManaType.Hell && m_inventory.HellStore >= m_spawnCosts[m_buildingLevel])
            {
                canSpawn = true;
                m_inventory.HellStore -= m_spawnCosts[m_buildingLevel];
            }
        }
        if (canSpawn)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
            PawnMana manaData = m_pawnObject.GetComponent<PawnMana>();
            Minions minionData = m_pawnObject.GetComponent<Minions>();

            if (pawnMergeScript)
            {
                m_minionType = m_minionData[minionIndex].m_minionType;
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_minionData[minionIndex].m_manaType, m_buildingType, m_itemType, m_minionData[minionIndex].m_sacrificeType, m_enemyTypes, m_rewardType, m_storeType,
                    m_minionData[minionIndex].m_pawnLevels, levelToSpawn);
                pawnMergeScript.GetPawnDataIndex = minionIndex; ;
                manaData.SetManaValues(m_minionData[minionIndex].m_manaType, m_minionData[minionIndex].m_baseMana, m_minionData[minionIndex].m_manaMultiplier);
                minionData.Damage = m_minionData[minionIndex].m_damageBaseValue;
                minionData.DamageMultiplier = m_minionData[minionIndex].m_damageMultiplier;
            }
            return true;
        }
        return false;
    }
    private void SetEnemyType(bool newPawn, int tileNum)
    {
        int indexToSpawn = -1;
        string enemyName = "";
        if (m_enemyTypes == PawnDefinitions.MEnemyTypes.Undead)
        {
            enemyName = "NecroEnemy";
        }
        else if (m_enemyTypes == PawnDefinitions.MEnemyTypes.Life)
        {
            enemyName = "LifeEnemy";
        }
        else if (m_enemyTypes == PawnDefinitions.MEnemyTypes.Hell)
        {
            enemyName = "HellEnemy";
        }

        if (enemyName == "")
        {
            Debug.Log("early exit");
            return;
        }
        for (int i = 0; i < m_enemyTypesCount; i++)
        {
            if (enemyName != "Empty" && m_enemyData[i].m_name == enemyName)
            {
                indexToSpawn = i;
            }
        }
        if (m_enemyTypes != PawnDefinitions.MEnemyTypes.Empty)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
            if (pawnMergeScript)
            {
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType,
                    m_itemType, m_enemyData[indexToSpawn].m_sacrificeType, m_enemyData[indexToSpawn].m_enemyTypes, m_rewardType, m_storeType,
                    m_enemyData[indexToSpawn].m_pawnLevel, 0);
                pawnMergeScript.GetPawnDataIndex = indexToSpawn;
            }
            m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_enemyData[indexToSpawn].m_sacrificialBaseValue, m_enemyData[indexToSpawn].m_sacrificeType, m_enemyData[indexToSpawn].m_sacrificialMultiplier);
            m_pawnObject.GetComponent<PawnEnemy>().SetEnemyValues(m_enemyData[indexToSpawn].m_enemyTypes,
                m_enemyData[indexToSpawn].m_manaAttraction, m_enemyData[indexToSpawn].m_health, m_enemyData[indexToSpawn].m_rewardBuildingName);
        }
    }
    private void SetRewardType(bool newPawn, int tileNum)
    {
        int indexToSpawn = -1;
        string RewardName = "";
        if (m_rewardType == PawnDefinitions.MRewardType.Money)
        {
            RewardName = "Money";
        }
        else if (m_rewardType == PawnDefinitions.MRewardType.DeathChest)
        {
            RewardName = "DeathRuneChest";
        }
        else if (m_rewardType == PawnDefinitions.MRewardType.LifeChest)
        {
            RewardName = "LifeRuneChest";
        }
        else if (m_rewardType == PawnDefinitions.MRewardType.HellChest)
        {
            RewardName = "HellRuneChest";
        }

        if (RewardName == "")
        {
            Debug.Log("early exit");
            return;
        }
        for (int i = 0; i < m_rewardTypesCount; i++)
        {
            if (RewardName != "Empty" && m_rewardData[i].m_name == RewardName)
            {
                indexToSpawn = i;
            }
        }
        if (m_rewardType != PawnDefinitions.MRewardType.Empty)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();
            if (pawnMergeScript)
            {
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType,
                    m_itemType, m_rewardData[indexToSpawn].m_sacrificeType, m_enemyTypes, m_rewardData[indexToSpawn].m_rewardType, m_storeType,
                    m_rewardData[indexToSpawn].m_pawnLevel, 0);
                pawnMergeScript.GetPawnDataIndex = indexToSpawn;
            }
            m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_rewardData[indexToSpawn].m_sacrificialBaseValue, m_rewardData[indexToSpawn].m_sacrificeType, m_rewardData[indexToSpawn].m_sacrificialMultiplier);
        }
    }
    private void SetStoreType(bool newPawn, int tileNum, string pawnType)
    {
        int indexToSpawn = -1;
        string storeName = "";

        if (newPawn && !Costs())
        {
            return;
        }
        if (m_storeType == PawnDefinitions.MStoreType.NecroStore)
        {
            storeName = "NecroStore";
        }
        else if (m_storeType == PawnDefinitions.MStoreType.LifeStore)
        {
            storeName = "LifeStore";
        }
        else if (m_storeType == PawnDefinitions.MStoreType.HellStore)
        {
            storeName = "HellStore";
        }

        if (storeName == "")
        {
            Debug.Log("early exit");
            return;
        }
        for (int i = 0; i < m_storeData.Count; i++)
        {
            if (m_storeData[i].m_name == storeName)
            {
                indexToSpawn = i;
            }
        }

        if (m_storeType != PawnDefinitions.MStoreType.Empty)
        {
            CreateObject(tileNum);
            PawnMerge pawnMergeScript = m_pawnObject.GetComponent<PawnMerge>();

            if (pawnMergeScript)
            {
                pawnMergeScript.SetPawnValues(m_objectType, m_minionType, m_manaType, m_buildingType,
                    m_itemType, m_storeData[indexToSpawn].m_sacrificeType, m_enemyTypes, m_rewardType, m_storeData[indexToSpawn].m_storeType,
                    m_storeData[indexToSpawn].m_pawnLevel, 0);
                pawnMergeScript.GetPawnDataIndex = indexToSpawn;
            }
            m_pawnObject.GetComponent<PawnSacrifice>().SetPawnValues(m_storeData[indexToSpawn].m_sacrificialBaseValue, m_storeData[indexToSpawn].m_sacrificeType, m_storeData[indexToSpawn].m_sacrificialMultiplier);
            m_pawnObject.GetComponent<PawnStore>().SetValues(m_storeData[indexToSpawn].m_storeType, m_storeData[indexToSpawn].m_manaCapacityIncrease, m_storeData[indexToSpawn].m_manaCapacityMultiplier);
        }
    }
    private void LevelUpPawn(int levelToSpawn)
    {
        for (int i = 0; i < levelToSpawn; i++)
        {
            GameEvents.m_current.PawnLevelUp(m_pawnObject.GetComponent<ID>().GetID);
        }
    }
    private void ResetTypes()
    {
        //m_minionType = PawnDefinitions.MMinionType.Empty;
        //m_itemType = PawnDefinitions.MItemType.Empty;
        m_buildingType = PawnDefinitions.MBuildingType.Empty;
    }
    private void LevelUpBuildingLevel(string id)
    {
        if (id == m_id)
        {
            m_buildingLevel++;
        }
    }
    private bool Costs()
    {
        bool canSpawn = true;

        if (m_inventory.Rune.m_deathRuneCount < m_buildingData.RuneCost.m_graveRuneCost)
        {
            canSpawn = false;
        }
        if (m_inventory.Rune.m_lifeRuneCount < m_buildingData.RuneCost.m_lifeRuneCost)
        {
            canSpawn = false;
        }
        if (m_inventory.Rune.m_hellRuneCount < m_buildingData.RuneCost.m_hellRuneCost)
        {
            canSpawn = false;
        }

        if (canSpawn)
        {
            m_inventory.DeathRune -= m_buildingData.RuneCost.m_graveRuneCost;
            m_inventory.LifeRune -= m_buildingData.RuneCost.m_lifeRuneCost;
            m_inventory.HellRune -= m_buildingData.RuneCost.m_hellRuneCost;

        }
        return canSpawn;

    }
}
