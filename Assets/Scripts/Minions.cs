
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    
    [SerializeField] private float m_damage;
    [SerializeField] private float m_damageMultiplier;
    [SerializeField] private ID m_id;

    public float Damage
    {
        get => m_damage;
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
        m_id = GetComponent<ID>();
    }
    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpValues;
    }
    private void LevelUpValues(string id)
    {
        if (m_id.GetID == id)
        {
            m_damage *= m_damageMultiplier;
        }
    }
}
