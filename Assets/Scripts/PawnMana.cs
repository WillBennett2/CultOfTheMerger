using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMana : MonoBehaviour
{
    [SerializeField]private int m_id;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private float m_baseMana;
    [SerializeField] private float m_manaMultiplier;

    [SerializeField]private Inventory m_inventoryScript;
    
    
    // Start is called before the first frame update

    public void SetManaValues(PawnDefinitions.MManaType manaType, float baseMana, float manaMultiplier)
    {
        m_manaType = manaType;
        m_baseMana = baseMana;
        m_manaMultiplier = manaMultiplier;
    }
    
    void Start()
    {
        if (GetComponent<Minions>())
        {
            m_id = GetComponent<Minions>().ID;   
        }

        GameEvents.m_current.onMinionLevelUp += LevelUpManaGen;
        m_inventoryScript = FindObjectOfType<Inventory>();
        
        
        m_inventoryScript.NecroModifier = m_baseMana;
    }

    private void OnDestroy()
    {
        GameEvents.m_current.onMinionLevelUp -= LevelUpManaGen;
        m_inventoryScript.NecroModifier = -m_baseMana;
    }

    private void LevelUpManaGen(int id)
    {
        if (id == m_id)
        {
            m_inventoryScript.NecroModifier = -m_baseMana;
            m_baseMana *= m_manaMultiplier;
            m_inventoryScript.NecroModifier = +m_baseMana;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
