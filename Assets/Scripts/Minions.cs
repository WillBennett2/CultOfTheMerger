
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_damageMultiplier;
    [SerializeField] private string m_id;
    [SerializeField]private PawnMerge m_pawnMergeScript;

    public float Damage
    {
        get
        {
            if(m_pawnMergeScript.GetPawnLevels < 2)
            {
                return 0f;
            }
            return m_damage;
        }
        set => m_damage = value;
    }
    public float DamageMultiplier
    {
       get => m_damageMultiplier;
       set => m_damageMultiplier = value; 
    }
    private void Awake()
    {
        GameEvents.m_current.onPawnLevelUp += LevelUpValues;
        m_id = GetComponent<ID>().GetID;
        m_pawnMergeScript = GetComponent<PawnMerge>();
    }

    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpValues;
    }
    private void LevelUpValues(string id)
    {
        if (m_id == id && m_pawnMergeScript.GetPawnLevels > 1)
        {
            m_damage *= m_damageMultiplier;
        }
    }
}
