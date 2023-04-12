using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LootSo", menuName = "ScriptableObjects/LootData")]
public class LootSo : ScriptableObject
{
    [System.Serializable]public struct Loot
    {
        public ObjectData m_lootType;
        public ObjectData.Item m_loot;
        public int m_spawnChance;

        public Loot(ObjectData lootType, int spawnChance)
        {
            m_lootType = lootType;
            m_loot = m_lootType.Items[0];
            m_spawnChance = spawnChance;
        }
    }

    [SerializeField] public Loot m_loot;
}
