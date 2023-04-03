using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData")]
public class ObjectData : ScriptableObject
{

    [System.Serializable]public struct Minion
    {
        [SerializeField] private string m_name;
        public PawnDefinitions.MMinionType m_minionType;
        public PawnLevels m_pawnLevels;
        public PawnDefinitions.MManaType m_manaType;
        public float m_baseMana;
        public float m_manaMultiplier;
        public PawnDefinitions.MSacrificeTypes m_sacrificeType;
        public float m_sacrificialBaseValue;
        public float m_sacrificialMultiplier;
    }
    
    [SerializeField] private Minion[] m_minions;

    public Minion[] Minions {
        set
        {
            m_minions = value;
        }
        get
        {
            return m_minions;
        }
    }
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