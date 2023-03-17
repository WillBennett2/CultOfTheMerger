using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private bool m_generateMana;
    [SerializeField] private float m_genDelay;
    [SerializeField] private float m_necroStore;
    [SerializeField] private float m_totalNecroModifier;
    [SerializeField] private float m_necroCapacity;
    [SerializeField] private float manaStore2;
    [SerializeField] private float manaStore3;


    public float Necro
    {
        set
        {
            m_totalNecroModifier += value;
        }
        get
        {
            
            return 0;
        }
    }

    private IEnumerator Start()
    {
        while (m_generateMana)
        {
            GenerateMana();
            yield return new WaitForSeconds(m_genDelay);
        }
        
    }

    private void GenerateMana()
    {
        if (m_necroCapacity > m_necroStore)
        {
            m_necroStore += m_totalNecroModifier;
        }
    }

    void Update()
    {
    }
}
