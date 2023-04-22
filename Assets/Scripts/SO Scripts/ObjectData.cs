using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        public float m_damageBaseValue;
        public float m_damageMultiplier;
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
   
    [System.Serializable]public struct Reward
    {
        [SerializeField] private string m_name;
        public PawnDefinitions.MRewardType m_rewardType;
        public ItemData m_loot;
        public PawnLevels m_pawnLevel;
        public PawnDefinitions.MSacrificeTypes m_sacrificeType;
        public float m_sacrificialBaseValue;
        public float m_sacrificialMultiplier;
    }
    [SerializeField] private Reward[] m_rewards;

    public Reward[] Rewards {
        set
        {
            m_rewards = value;
        }
        get
        {
            return m_rewards;
        }
    }

    [System.Serializable]
    public struct Enemy
    {
        [SerializeField] private string m_name;
        public PawnDefinitions.MEnemyTypes m_enemyTypes;
        public PawnDefinitions.MManaType m_manaAttraction;
        public float m_health;
        public GameObject m_reward;
        public PawnLevels m_pawnLevel;
        public PawnDefinitions.MSacrificeTypes m_sacrificeType;
        public float m_sacrificialBaseValue;
        public float m_sacrificialMultiplier;
    }

    [SerializeField] private Enemy[] m_enemies;
    public Enemy[] Enemies {
        set
        {
            m_enemies = value;
        }
        get
        {
            return m_enemies;
        }
    }
}