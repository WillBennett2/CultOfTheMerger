using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSacrifice : MonoBehaviour
{
    [SerializeField] private int m_id;
    [SerializeField] private float m_baseSacrificialValue;
    [SerializeField] private float m_sacrificeMultiplier;
    [SerializeField] private Inventory m_inventoryScript;


    public void SetPawnValues(float baseSacrificalValue,float sacrificeMultiplier)
    {
        m_baseSacrificialValue = baseSacrificalValue;
        m_sacrificeMultiplier = sacrificeMultiplier;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Minions>())
        {
            m_id = gameObject.GetComponent<Minions>().ID;   
        }
        m_inventoryScript = FindObjectOfType<Inventory>();
    }

    void Sacrificed()
    {
        m_inventoryScript.SacrificeValue = Mathf.RoundToInt(m_baseSacrificialValue);
    }

}
