
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    
    [SerializeField] private int m_id;
    [SerializeField] private float m_damage;
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
}
