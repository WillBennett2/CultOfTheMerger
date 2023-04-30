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
    [SerializeField] private List<string> m_Data;
    [SerializeField] private List<ListWrapper> m_dataList;
    [SerializeField] private bool m_load;
    [SerializeField] private Buildings m_graveMinionBuilding;
    [SerializeField] private Buildings m_buildingBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_load)
        {
            LoadPawns();
            m_load = false;
        }
    }

    public void SaveMoveablePawns(PawnMovement pawnMovementScript)
    {
        string key = "homeTile"+pawnMovementScript.GetHomeTileNum.ToString();
        PlayerPrefs.SetInt(key, pawnMovementScript.GetHomeTileNum );
    }
    public void LoadMoveablePawns()
    {
    //    PlayerPrefs.GetFloat()
    }

    public void SavePawns(List<PawnMovement> pawns)
    {
        for(int i = 0; i < pawns.Count; i++)
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

            m_Data.Add( data); 
        }
    }
    public void LoadPawns()
    {
        //load where it is on the grid
        for (int i = 0;i<m_Data.Count;i++)
        {
            m_dataList.Add(new ListWrapper());
            m_dataList[i].m_data = m_Data[i].Split("_").ToList();
        }

        for (int i = 0; i < m_dataList.Count; i++)
        {
            if (m_dataList[i][1]=="Minions")
            {
                m_graveMinionBuilding.LoadPawn(int.Parse(m_dataList[i][0]), int.Parse(m_dataList[i][2]), int.Parse(m_dataList[i][3]));
            }
            
        }
    }
}
