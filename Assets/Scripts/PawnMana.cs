using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMana : MonoBehaviour
{
    [SerializeField]private string m_id;
    [SerializeField] private PawnDefinitions.MManaType m_manaTypeCost;
    [SerializeField] private PawnDefinitions.MManaType m_manaTypeGenerated;
    [SerializeField] private float m_baseMana;
    [SerializeField] private float m_manaMultiplier;

    private Inventory m_inventoryScript;
    private PawnMerge m_pawnMergeScript;

    // Start is called before the first frame update

    public void SetManaValues(PawnDefinitions.MManaType manaTypeCost, PawnDefinitions.MManaType manaTypeGen, float baseMana, float manaMultiplier)
    {
        m_manaTypeCost = manaTypeCost;
        m_manaTypeGenerated = manaTypeGen;
        m_baseMana = baseMana;
        m_manaMultiplier = manaMultiplier;
    }
    private void Awake()
    {
        GameEvents.m_current.onPawnLevelUp += LevelUpManaGen;
    }
    void Start()
    {
        if (GetComponent<ID>())
        {
            m_id = GetComponent<ID>().GetID;   
        }

        m_inventoryScript = FindObjectOfType<Inventory>();
        m_pawnMergeScript = GetComponent<PawnMerge>();


        if (m_pawnMergeScript.GetPawnLevels >= 2)
        {
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Necro)
            {
                m_inventoryScript.NecroModifier += m_baseMana;
            }
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Life)
            {
                m_inventoryScript.LifeModifier += m_baseMana;
            }
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Hell)
            {
                m_inventoryScript.HellModifier += m_baseMana;
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpManaGen;
        if (m_pawnMergeScript.GetPawnLevels >= 2)
        {

            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Necro)
            {
                m_inventoryScript.NecroModifier -= m_baseMana;
            }
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Life)
            {
                m_inventoryScript.LifeModifier -= m_baseMana;
            }
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Hell)
            {
                m_inventoryScript.HellModifier -= m_baseMana;
            }
        }
        
    }

    private void LevelUpManaGen(string id)
    {
        if (id == m_id && m_pawnMergeScript.GetPawnLevels >= 2)
        {

            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Necro)
            {
                m_inventoryScript.NecroModifier -= m_baseMana;
                m_baseMana *= m_manaMultiplier;
                m_inventoryScript.NecroModifier += m_baseMana;
            }
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Life)
            {
                m_inventoryScript.LifeModifier -= m_baseMana;
                m_baseMana *= m_manaMultiplier;
                m_inventoryScript.LifeModifier += m_baseMana;
            }
            if (m_manaTypeGenerated == PawnDefinitions.MManaType.Hell)
            {
                m_inventoryScript.HellModifier -= m_baseMana;
                m_baseMana *= m_manaMultiplier;
                m_inventoryScript.HellModifier += m_baseMana;
            }

        }
    } 
    

}
