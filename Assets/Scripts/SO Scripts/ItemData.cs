using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    [System.Serializable]public struct Item
    {
        [SerializeField] private string m_name;
        public PawnDefinitions.MItemType m_itemType;
        public PawnLevels m_pawnLevels;
        public PawnDefinitions.MSacrificeTypes m_sacrificeType;
        public float m_sacrificialBaseValue;
        public float m_sacrificialMultiplier;
    }
    
    [SerializeField] private Item[] m_items;

    public Item[] Items {
        set
        {
            m_items = value;
        }
        get
        {
            return m_items;
        }
    }
}
