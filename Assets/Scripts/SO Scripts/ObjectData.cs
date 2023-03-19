using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData")]
public class ObjectData : ScriptableObject
{
    [System.Serializable]public struct Minion
    {
        public PawnDefinitions.MMinionType m_minionType;
        public PawnLevels m_pawnLevels;
        public PawnDefinitions.MManaType m_manaType;
        public float m_baseMana;
        public float m_manaMultiplier;
        public float m_manaCost;
        public float m_sacrificialBaseValue;
        public float m_sacrificialMultiplier;
    }
    
    [SerializeField] private Minion[] m_objects;

    public Minion[] Objects {
        set
        {
            m_objects = value;
        }
        get
        {
            return m_objects;
        }
    }
    
}