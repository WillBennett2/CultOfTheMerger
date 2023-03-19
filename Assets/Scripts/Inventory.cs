using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private bool m_generateMana;
    [SerializeField] private float m_genDelay;
    [Header("Necro")]
    [SerializeField] private float m_necroStore;
    private float m_previousNecroStore;
    [SerializeField] private float m_totalNecroModifier;
    [SerializeField] private float m_necroCapacity;
    [SerializeField]private TextMeshProUGUI m_necroUIText;
    [SerializeField] private Slider m_necroSlider;
    [Header("Life")]
    [SerializeField] private float m_lifeStore;
    [SerializeField] private float m_totalLifeModifier;
    [SerializeField] private float m_lifeCapacity;
    [SerializeField] private TextMeshProUGUI m_lifeUIText;
    [SerializeField] private Slider m_lifeSlider;
    [Header("Store3")]
    [SerializeField] private float manaStore3;
    [Header("Shop")]
    [SerializeField] private int coinCount;
    [SerializeField] private int gemCount;
    

    public float NecroModifier
    {
        set
        {
            m_totalNecroModifier += value;
        }
        get
        {
            return m_totalNecroModifier;
        }
    }
    public float NecroStore
    {
        set
        {
            m_necroStore += value;
            UpdateUI();
        }
        get
        {
            return m_necroStore;
        }
    }

    private IEnumerator Start()
    {
        m_necroSlider.value = m_necroStore;
        m_necroUIText.text = Mathf.Round(m_necroStore).ToString();
        m_necroSlider.maxValue = m_necroCapacity;
        
        while (m_generateMana)
        {
            GenerateMana();
            UpdateUI();
            yield return new WaitForSeconds(m_genDelay);
        }
        
    }

    private void GenerateMana()
    {
        if (m_necroCapacity > m_necroStore)
        {
            NecroStore = m_totalNecroModifier;
        }
    }

    void UpdateUI()
    {
        m_necroSlider.value = m_necroStore;
        m_necroUIText.text = Mathf.Round(m_necroStore).ToString();
    }
    void Update()
    {

    }
}
