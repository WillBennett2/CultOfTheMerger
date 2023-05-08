using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static PawnDefinitions;

public class PawnStore : MonoBehaviour
{
    private string m_id;
    private Inventory m_inventoryScript;
    [SerializeField] private PawnDefinitions.MStoreType m_storeType;
    [SerializeField] private float m_manaCapacityStore;
    [SerializeField] private float m_manaCapacityMultiplier;



    public void SetValues(PawnDefinitions.MStoreType storeType, float manaCapStore, float manaCapMultiplier)
    {
        m_storeType = storeType;
        m_manaCapacityStore = manaCapStore;
        m_manaCapacityMultiplier = manaCapMultiplier;
        IncreaseManaStore();
    }

    private void Awake()
    {
        GameEvents.m_current.onPawnLevelUp += LevelUpValues;
        m_inventoryScript = Camera.main.GetComponent<Inventory>();
        m_id = GetComponent<ID>().GetID;
    }
    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpValues;
        DecreaseManaStore();
    }
    private void LevelUpValues(string id)
    {
        if (id == m_id)
        {
            DecreaseManaStore();
            m_manaCapacityStore *= m_manaCapacityMultiplier;
            IncreaseManaStore();
        }   
    }
    private void IncreaseManaStore()
    {
        if (m_storeType == PawnDefinitions.MStoreType.NecroStore)
        {
            m_inventoryScript.NecroCapacity += m_manaCapacityStore;
        }
        else if (m_storeType == PawnDefinitions.MStoreType.LifeStore)
        {
            m_inventoryScript.LifeCapacity += m_manaCapacityStore;
        }
        else if (m_storeType == PawnDefinitions.MStoreType.HellStore)
        {
            m_inventoryScript.HellCapacity += m_manaCapacityStore;
        }
    }
    private void DecreaseManaStore()
    {
        if (m_storeType == PawnDefinitions.MStoreType.NecroStore)
        {
            m_inventoryScript.NecroCapacity -= m_manaCapacityStore;
        }
        else if (m_storeType == PawnDefinitions.MStoreType.LifeStore)
        {
            m_inventoryScript.LifeCapacity -= m_manaCapacityStore;
        }
        else if (m_storeType == PawnDefinitions.MStoreType.HellStore)
        {
            m_inventoryScript.HellCapacity -= m_manaCapacityStore;
        }
    }

}
