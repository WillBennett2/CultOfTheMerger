
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    
    [SerializeField] private int m_id;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_damageMultiplier;
    public int ID
    {
        get => m_id;
        set => m_id = value;
    }
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
    private void Start()
    {
        GameEvents.m_current.onMinionLevelUp += LevelUpValues;
    }
    private void OnDestroy()
    {
        GameEvents.m_current.onMinionLevelUp -= LevelUpValues;
    }
    private void LevelUpValues(int id)
    {
        if (m_id == id)
        {
            m_damage *= m_damageMultiplier;
        }
    }
}
