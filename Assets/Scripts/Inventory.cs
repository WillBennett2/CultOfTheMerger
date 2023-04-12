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
    [Header("Hell")]
    [SerializeField] private float m_hellStore;
    [SerializeField] private float m_totalHellModifier;
    [SerializeField] private float m_hellCapacity;
    [SerializeField] private TextMeshProUGUI m_hellUIText;
    [SerializeField] private Slider m_hellSlider;

    [Header("Sacrifice")] 
    [SerializeField] private int m_cultSacrificeValue;
    [SerializeField] private int m_maxCultValue;
    [SerializeField]private TextMeshProUGUI m_cultValueUIText;
    [SerializeField]private TextMeshProUGUI m_MaxCultValueUIText;
    [SerializeField] private Slider m_cultValueSlider;
    [Header("Shop")]
    [SerializeField] private int m_coinCount;
    [SerializeField] private int m_gemCount;
    [SerializeField] public Runes m_runes;

    [Serializable]public struct Runes
    {
        [SerializeField] public int m_deathRuneCount;
        [SerializeField] public int runeCount2;
        [SerializeField] public int runeCount3;
        [SerializeField] public int runeCount4;
    }

    public Runes Rune
    {
        get
        {
            return m_runes;
        }
    }
    public int DeathRune
    {
        get
        {
            return m_runes.m_deathRuneCount;
        }
        set
        {
            m_runes.m_deathRuneCount += value;
        }
    }
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
            UpdateManaUI();
        }
        get
        {
            return m_necroStore;
        }
    }

    public int SacrificeValue
    {
        get
        {
            return m_cultSacrificeValue;
        }
        set
        {
            m_cultSacrificeValue += value;
            UpdateGeneralUI();
        }
    }
    public int Coins
    {
        get
        {
            return m_coinCount;
        }
        set
        {
            m_coinCount += value;
        }
    }
    private IEnumerator Start()
    {
        m_necroSlider.value = m_necroStore;
        m_necroUIText.text = Mathf.Round(m_necroStore).ToString();
        m_necroSlider.maxValue = m_necroCapacity;

        m_cultValueSlider.value = m_cultSacrificeValue;
        m_cultValueSlider.maxValue = m_maxCultValue;
        m_cultValueUIText.text = Mathf.Round(m_cultSacrificeValue).ToString();
        m_MaxCultValueUIText.text = ChangeUINumber(m_maxCultValue).ToString()+ "K";
        
        while (m_generateMana)
        {
            GenerateMana();
            UpdateManaUI();
            yield return new WaitForSeconds(m_genDelay);
        }
        
    }

    private void GenerateMana()
    {
        if (m_necroCapacity > m_necroStore)
        {
            NecroStore = m_totalNecroModifier;
        }
        else
        {
            m_necroStore = m_necroCapacity;
        }
    }

    void UpdateManaUI()
    {
        m_necroSlider.value = m_necroStore;
        m_necroUIText.text =ChangeUINumber((int) m_necroStore).ToString()+"K";
    }

    void UpdateGeneralUI()
    {
        m_cultValueSlider.value = m_cultSacrificeValue;
        m_cultValueUIText.text = Mathf.Round(ChangeUINumber(m_cultSacrificeValue)).ToString();
    }

    int ChangeUINumber(int value)
    {
        if (value >= 1000)
        {
            value /= 1000;
        }

        return value;
    }
}
