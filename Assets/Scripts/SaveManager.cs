using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    [Serializable]public class ListWrapper
    {
        public List<string> m_data;

        public string this[int key]
        {
            get
            {
                return m_data[key];
            }
            set
            {
                m_data[key] = value;
            }
        }
    }

    private Inventory m_inventoryScript;
    private GameManager m_gameManagerScript;

    [SerializeField] private List<string> m_Data;
    [SerializeField] private List<ListWrapper> m_dataList;
    [SerializeField] private bool m_load;
    [SerializeField] private bool m_delete;
    [SerializeField] private Buildings m_graveMinionBuilding;
    [SerializeField] private Buildings m_graveBuilding;
    [SerializeField] private Buildings m_lifeBuilding;
    [SerializeField] private Buildings m_hellBuilding;

    [SerializeField] private Buildings m_CoinBuilding;
    [SerializeField] private Buildings m_GemBuilding;
    [SerializeField] private Buildings m_NecroRuneBuilding;
    [SerializeField] private Buildings m_LifeRuneBuilding;
    [SerializeField] private Buildings m_HellRuneBuilding;
    [SerializeField] private Buildings m_NecroPotionBuilding;
    [SerializeField] private Buildings m_LifePotionBuilding;
    [SerializeField] private Buildings m_HellPotionBuilding;
    //[SerializeField] private Buildings m_NecroRuneBuilding;

    [SerializeField] private Buildings m_NecroChestBuilding;

    [SerializeField] private Buildings m_undeadEnemyBuilding;
    [SerializeField] private Buildings m_lifeEnemyBuilding;
    [SerializeField] private Buildings m_hellEnemyBuilding;

    [SerializeField] private Buildings m_NecroCapacityBuilding;
    [SerializeField] private Buildings m_LifeCapacityBuilding;
    [SerializeField] private Buildings m_HellCapacityBuilding;

    [SerializeField] private DateTime m_currentDate;
    [SerializeField] private DateTime m_previousDate;

    private void Start()
    {
        m_gameManagerScript = GetComponent<GameManager>();  
        m_inventoryScript = FindObjectOfType<Inventory>();

    }
    private void OnApplicationFocus(bool focusings)
    {
        if (!focusings)
        {
            PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
            Debug.Log("current time saved" + System.DateTime.Now);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(m_delete)
        {
            DeletePrefs();
            m_delete = false;
        }
    }


    public void SaveMoveablePawns(PawnMovement pawnMovementScript)
    {
        string key = "homeTile"+pawnMovementScript.GetHomeTileNum.ToString();
        PlayerPrefs.SetInt(key, pawnMovementScript.GetHomeTileNum );
    }

    public void SavePawns(List<PawnMovement> pawns)
    {
        //DeletePrefs();
        for (int i = 0; i < pawns.Count; i++)
        {
            string key = "pawn"+ i.ToString();
            string data;

            //save where it is on the grid
            data = pawns[i].GetHomeTileNum.ToString() + "_";
            //save what type of pawn it is
            data += pawns[i].GetPawnMerge.GetPawnObjectType.ToString()+"_"; 
            //save what index it is
            data += pawns[i].GetPawnMerge.GetPawnDataIndex.ToString() + "_"; 
            //save waht level it is
            data += pawns[i].GetPawnMerge.GetPawnLevels.ToString() + "_";
            //save what minion type it is
            data += pawns[i].GetPawnMerge.GetMinionType.ToString() + "_";

            //save what type of item it is
            data += pawns[i].GetPawnMerge.GetItemType.ToString() + "_";
            //save what type of reward it is
            data += pawns[i].GetPawnMerge.GetRewardType.ToString() + "_";
            //save what type of enemy it is
            data += pawns[i].GetPawnMerge.GetEnemyType.ToString() + "_";
            //save what type of store it is
            data += pawns[i].GetPawnMerge.GetStoreType.ToString() + "_";

            //m_Data.Add( data); 
            //save to player prefs
            PlayerPrefs.SetString(key, data);
        }
        PlayerPrefs.SetInt("PawnNum", pawns.Count);
        SaveStats();
        PlayerPrefs.Save();
        Debug.Log("save data");
    }
    private void SaveStats()
    {
        PlayerPrefs.SetFloat("NecroStore",m_inventoryScript.NecroStore);
        PlayerPrefs.SetFloat("LifeStore", m_inventoryScript.LifeStore);
        PlayerPrefs.SetFloat("HellStore", m_inventoryScript.HellStore);
        PlayerPrefs.SetInt("SacrificeValue", m_inventoryScript.SacrificeValue);
        PlayerPrefs.SetInt("DeathRune", m_inventoryScript.DeathRune);
        PlayerPrefs.SetInt("LifeRune", m_inventoryScript.LifeRune);
        PlayerPrefs.SetInt("HellRune", m_inventoryScript.HellRune);
        PlayerPrefs.SetInt("SpecialRune", m_inventoryScript.SpecialRune);
        PlayerPrefs.SetInt("Coin", m_inventoryScript.Coins);
        PlayerPrefs.SetInt("Gem", m_inventoryScript.Gems);
        PlayerPrefs.SetInt("UndeadMinionsSpawned", m_gameManagerScript.GetNumOfUndeadSpawned);

        PlayerPrefs.SetString("TimeGiftWasClaimed", System.DateTime.Now.ToBinary().ToString());

    }
    public void LoadPawns()
    {
        //load from player prefs
        Debug.Log("load data");
        for (int i = 0;i<PlayerPrefs.GetInt("PawnNum");i++)
        {
            m_dataList.Add(new ListWrapper());
            string key = "pawn" + i.ToString();
            m_dataList[i].m_data = PlayerPrefs.GetString(key).Split("_").ToList();
            //m_dataList[i].m_data = m_Data[i].Split("_").ToList();
        }

        for (int i = 0; i < m_dataList.Count; i++)
        {
            if (m_dataList[i][1]=="Minions")
            {
                m_graveMinionBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
            }
            if(m_dataList[i][1] == "Building")
            {
                if(m_dataList[i][4] == "Undead")
                {
                    m_graveBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if(m_dataList[i][4] == "Plant")
                {
                    m_lifeBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][4] == "Demon")
                {
                    m_hellBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
            }
            if(m_dataList[i][1] =="Item")
            {
                if (m_dataList[i][5] == "Coin")
                {
                    m_CoinBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][5] == "Gem")
                {
                    m_GemBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][5] == "DeathRune")
                {
                    Debug.Log("DeathRuneLoad");
                    m_NecroRuneBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][5] == "LifeRune")
                {
                    m_LifeRuneBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][5] == "HellRune")
                {
                    m_HellRuneBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }

            }
            if (m_dataList[i][1] == "Reward")
            {
                if (m_dataList[i][6] == "HellRune")
                {
                    m_NecroChestBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
            }
            if (m_dataList[i][1] == "Enemy")
            {
                if (m_dataList[i][7] == "Undead")
                {
                    m_undeadEnemyBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][7] == "Life")
                {
                    m_lifeEnemyBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][7] == "Hell")
                {
                    m_hellEnemyBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
            }
            if (m_dataList[i][1] == "Store")
            {
                if (m_dataList[i][8] == "NecroStore")
                {
                    m_NecroCapacityBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][8] == "LifeStore")
                {
                    m_LifeCapacityBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
                else if (m_dataList[i][8] == "HellStore")
                {
                    m_HellCapacityBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]),
                    m_dataList[i][4], m_dataList[i][5], m_dataList[i][6], m_dataList[i][7], m_dataList[i][8]);
                }
            }

        }
        LoadStats();
    }
    private void LoadStats()
    { 
        if(PlayerPrefs.GetFloat("NecroStore")==0)
        {
            m_inventoryScript.NecroStore = 3000;
        }
        else
            m_inventoryScript.NecroStore = PlayerPrefs.GetFloat("NecroStore");

        m_inventoryScript.LifeStore = PlayerPrefs.GetFloat("LifeStore");

        m_inventoryScript.HellStore = PlayerPrefs.GetFloat("HellStore");

        //save rune count
        m_inventoryScript.DeathRune = 100;// PlayerPrefs.GetInt("DeathRune");
        m_inventoryScript.LifeRune = PlayerPrefs.GetInt("LifeRune");
        m_inventoryScript.HellRune = PlayerPrefs.GetInt("HellRune");
        m_inventoryScript.SpecialRune = PlayerPrefs.GetInt("SpecialRune");
        //save coin coint
        m_inventoryScript.Coins = PlayerPrefs.GetInt("Coin");
        m_inventoryScript.Gems = PlayerPrefs.GetInt("Gem");
        //save cult count
        m_inventoryScript.SacrificeValue = PlayerPrefs.GetInt("SacrificeValue");

        //enemy spawns
        m_gameManagerScript.GetNumOfUndeadSpawned = PlayerPrefs.GetInt("UndeadMinionsSpawned");

        StartCoroutine(DelayTime(1));
    }
    IEnumerator DelayTime (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //load mana
        m_currentDate = DateTime.Now;
        if (PlayerPrefs.HasKey("sysString"))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
            m_previousDate = DateTime.FromBinary(temp);
        }
        else
        {
            DateTime temp = DateTime.Now;
            m_previousDate = temp;
        }
        
        TimeSpan difference = m_currentDate.Subtract(m_previousDate);
        Debug.Log(difference);
        float numOfGenerations = (int)difference.TotalSeconds / m_inventoryScript.GetGenerationDelay;
        Debug.Log("Number of generations missed " + numOfGenerations);
        Debug.Log("Before Num of necro mana " + m_inventoryScript.NecroStore);
        for (int i = 0; i < numOfGenerations; i++)
        {
            m_inventoryScript.GenerateMana();
        }
        Debug.Log("After Num of necro mana " + m_inventoryScript.NecroStore);
    }
    private void DeletePrefs()
    {
       PlayerPrefs.DeleteAll();
    }
}
