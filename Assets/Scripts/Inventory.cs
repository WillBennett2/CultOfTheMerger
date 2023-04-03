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
    [SerializeField] private float m_manaStore3;

    [Header("Sacrifice")] 
    [SerializeField] private int m_cultSacrificeValue;
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
            UpdateUI();
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
        else
        {
            m_necroStore = m_necroCapacity;
        }
    }

    void UpdateUI()
    {
        m_necroSlider.value = m_necroStore;
        m_necroUIText.text = Mathf.Round(m_necroStore).ToString();
    }
}
