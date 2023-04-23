using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectData;

[CreateAssetMenu(fileName = "SpawnChances", menuName = "ScriptableObjects/SpawnChances")]

public class SpawnChances : ScriptableObject
{
    [System.Serializable]public struct MinionTypeChances
    {
        public int m_spawnLevel;
        public MinionPartsChances[] m_chances;
    }
    [SerializeField] MinionTypeChances[] m_totalChances;
    public MinionTypeChances[] Chances
    {
        get
        {
            return m_totalChances;
        }
    }
    [System.Serializable]public struct MinionPartsChances
    {
        public string m_minionName;
        public int m_minionLevel;
        public int m_chance;
    }
}
