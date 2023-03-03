
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public PawnDefinitions.MManaType ManaType
    {
        get => m_manaType;
        set => m_manaType = value;
    }
}
