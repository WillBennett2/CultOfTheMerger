using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        m_gameManagerScript = GetComponent<GameManager>();  
        m_inventoryScript = FindObjectOfType<Inventory>();
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
                m_graveMinionBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]), m_dataList[i][4] );
            }
            if(m_dataList[i][1] == "Building")
            {
                if(m_dataList[i][4] == "Undead")
                {
                    m_graveBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]), m_dataList[i][4]);
                }
                else if(m_dataList[i][4] == "Plant")
                {
                    m_lifeBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]), m_dataList[i][4]);
                }
                else if (m_dataList[i][4] == "Demon")
                {
                    m_hellBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]), m_dataList[i][4]);
                }
            }
            
        }
        LoadStats();
    }
    private void LoadStats()
    {


        m_inventoryScript.NecroStore = PlayerPrefs.GetFloat("NecroStore");

        m_inventoryScript.LifeStore = PlayerPrefs.GetFloat("LifeStore");

        m_inventoryScript.HellStore = PlayerPrefs.GetFloat("HellStore");

        //save rune count
        m_inventoryScript.DeathRune = PlayerPrefs.GetInt("DeathRune");
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
    }
    private void DeletePrefs()
    {
       PlayerPrefs.DeleteAll();
    }
}
