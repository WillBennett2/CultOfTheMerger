using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMana : MonoBehaviour
{
    [SerializeField]private string m_id;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private float m_baseMana;
    [SerializeField] private float m_manaMultiplier;

    private Inventory m_inventoryScript;
    private PawnMerge m_pawnMergeScript;

    // Start is called before the first frame update

    public void SetManaValues(PawnDefinitions.MManaType manaType, float baseMana, float manaMultiplier)
    {
        m_manaType = manaType;
        m_baseMana = baseMana;
        m_manaMultiplier = manaMultiplier;
    }
    
    void Start()
    {
        if (GetComponent<ID>())
        {
            m_id = GetComponent<ID>().GetID;   
        }

        GameEvents.m_current.onPawnLevelUp += LevelUpManaGen;
        m_inventoryScript = FindObjectOfType<Inventory>();
        m_pawnMergeScript = GetComponent<PawnMerge>();



        m_inventoryScript.NecroModifier = m_baseMana;
    }

    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpManaGen;
        m_inventoryScript.NecroModifier = -m_baseMana;
    }

    private void LevelUpManaGen(string id)
    {
        if (id == m_id && m_pawnMergeScript.GetPawnLevels <= 2)
        {
            m_inventoryScript.NecroModifier = -m_baseMana;
            m_baseMana *= m_manaMultiplier;
            m_inventoryScript.NecroModifier = +m_baseMana;
        }
    }

}
