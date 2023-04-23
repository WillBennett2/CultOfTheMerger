using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSacrifice : MonoBehaviour
{
    [SerializeField] private string m_id;
    private ID m_idScript;
    [SerializeField] private float m_baseSacrificialValue;
    [SerializeField] private float m_sacrificeMultiplier;
    [SerializeField] private PawnDefinitions.MSacrificeTypes m_sacrificeTypes;
    private Inventory m_inventoryScript;

    private AlterSacrifice m_alter;


    public void SetPawnValues(float baseSacrificalValue,PawnDefinitions.MSacrificeTypes sacrificeTypes,float sacrificeMultiplier)
    {
        m_baseSacrificialValue = baseSacrificalValue;
        m_sacrificeMultiplier = sacrificeMultiplier;
        m_sacrificeTypes = sacrificeTypes;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.m_current.onPawnLevelUp += LevelUpValues;
        StartCoroutine(LateStart(1));
        m_inventoryScript = FindObjectOfType<Inventory>();
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_idScript = GetComponent<ID>();
        m_id = m_idScript.GetID;
    }
    private void OnDestroy()
    {
        GameEvents.m_current.onPawnLevelUp -= LevelUpValues;
    }
    private void LevelUpValues(string id)
    {
        if (id == m_id)
        {
            m_baseSacrificialValue *= m_sacrificeMultiplier;
        }
    }

    public void AttemptSacrifice()
    {
        if (m_alter) 
        {
            GameEvents.m_current.PawnSacrifice(gameObject,m_sacrificeTypes,m_baseSacrificialValue);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var script = other.GetComponent<AlterSacrifice>();
        if (script != null)
        {
            m_alter = script;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_alter = null;
    }
}
