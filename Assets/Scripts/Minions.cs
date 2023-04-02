
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    
    [SerializeField] private int m_id;
    public int ID
    {
        get => m_id;
        set => m_id = value;
    }
}
