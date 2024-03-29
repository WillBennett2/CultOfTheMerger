using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    [SerializeField] private bool m_generateMana;
    [SerializeField] private float m_genDelay;
    [Header("Necro")]
    [SerializeField] private float m_necroStore;
    [SerializeField] private float m_totalNecroModifier;
    [SerializeField] private float m_necroCapacity;
    [SerializeField]private Text m_necroUIText;
    [SerializeField] private Slider m_necroSlider;
    [Header("Life")]
    [SerializeField] private float m_lifeStore;
    [SerializeField] private float m_totalLifeModifier;
    [SerializeField] private float m_lifeCapacity;
    [SerializeField] private Text m_lifeUIText;
    [SerializeField] private Slider m_lifeSlider;
    [Header("Hell")]
    [SerializeField] private float m_hellStore;
    [SerializeField] private float m_totalHellModifier;
    [SerializeField] private float m_hellCapacity;
    [SerializeField] private Text m_hellUIText;
    [SerializeField] private Slider m_hellSlider;

    [Header("Sacrifice")]
    [SerializeField] private GooglePlayManager m_GPMScript;
    [SerializeField] private int m_cultSacrificeValue;
    [SerializeField] private int m_maxCultValue;
    [SerializeField]private Text m_cultValueUIText;
    [SerializeField]private Text m_MaxCultValueUIText;
    [SerializeField] private Slider m_cultValueSlider;
    [Header("Shop")]
    [SerializeField] private int m_coinCount;
    [SerializeField] private Text m_coinValueUIText;
    [SerializeField] private int m_gemCount;
    [SerializeField] private Text m_gemValueUIText;
    [SerializeField] public Runes m_runes;
    [SerializeField] private Text m_NecroRuneCount;
    [SerializeField] private Text m_LifeRuneCount;
    [SerializeField] private Text m_HellRuneCount;


    [Serializable]public struct Runes
    {
        [SerializeField] public int m_deathRuneCount;
        [SerializeField] public int m_lifeRuneCount;
        [SerializeField] public int m_hellRuneCount;
        [SerializeField] public int m_specialRuneCount;
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
            m_runes.m_deathRuneCount = value;
            m_NecroRuneCount.text = m_runes.m_deathRuneCount.ToString();
        }
    }
    public int LifeRune
    {
        get
        {
            return m_runes.m_lifeRuneCount;
        }
        set
        {
            m_runes.m_lifeRuneCount = value;
            m_LifeRuneCount.text = m_runes.m_lifeRuneCount.ToString();
        }
    }
    public int HellRune
    {
        get
        {
            return m_runes.m_hellRuneCount;
        }
        set
        {
            m_runes.m_hellRuneCount = value;
            m_HellRuneCount.text = m_runes.m_hellRuneCount.ToString();
        }
    }
    public int SpecialRune
    {
        get
        {
            return m_runes.m_specialRuneCount;
        }
        set
        {
            m_runes.m_specialRuneCount = value;
        }
    }
    public float NecroModifier
    {
        set
        {
            m_totalNecroModifier = value;
            if(m_totalHellModifier<0)
            {
                m_totalHellModifier = 0;
            }
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
            m_necroStore = value;
            UpdateManaUI();
        }
        get
        {
            return m_necroStore;
        }
    }
    public float NecroCapacity
    {
        set
        {
            m_necroCapacity = value;
            m_necroSlider.maxValue = m_necroCapacity;
        }
        get
        {
            return m_necroCapacity;
        }
    }
    public float HellModifier
    {
        set
        {
            m_totalHellModifier = value;
            if (m_totalHellModifier < 0)
            {
                m_totalHellModifier = 0;
            }
        }
        get
        {
            return m_totalHellModifier;
        }
    }
    public float HellStore
    {
        set
        {
            m_hellStore = value;
            UpdateManaUI();
        }
        get
        {
            return m_hellStore;
        }
    }
    public float HellCapacity
    {
        set
        {
            m_hellCapacity = value;
            m_hellSlider.maxValue = m_hellCapacity;
        }
        get
        {
            return m_hellCapacity;
        }
    }
    public float LifeModifier
    {
        set
        {
            m_totalLifeModifier = value;
            if (m_totalLifeModifier < 0)
            {
                m_totalLifeModifier = 0;
            }
        }
        get
        {
            return m_totalLifeModifier;
        }
    }
    public float LifeStore
    {
        set
        {
            m_lifeStore = value;
            UpdateManaUI();
        }
        get
        {
            return m_lifeStore;
        }
    }
    public float LifeCapacity
    {
        set
        {
            m_lifeCapacity = value;
            m_lifeSlider.maxValue = m_lifeCapacity;
        }
        get
        {
            return m_lifeCapacity;
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
            m_cultSacrificeValue = value;
            if(m_cultSacrificeValue>=m_maxCultValue)
            {
                //level up
                m_gameManager.LevelUpCult();
                m_maxCultValue *= 2;
                m_MaxCultValueUIText.text = m_maxCultValue.ToString();
				m_cultValueSlider.maxValue= m_maxCultValue;

			}
            m_GPMScript.UpdateLeaderBoardCultScore(m_cultSacrificeValue);
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
            m_coinCount = value;
            UpdateGeneralUI();
        }
    }
    public int Gems
    {
        get
        {
            return m_gemCount;
        }
        set
        {
            m_gemCount = value;
            UpdateGeneralUI();
        }
    }

    public float GetGenerationDelay
    {
        get { return m_genDelay; }
    }
    private IEnumerator Start()
    {
        m_necroSlider.value = m_necroStore;
        m_necroUIText.text = Mathf.Round(m_necroStore).ToString();
        m_necroSlider.maxValue = m_necroCapacity;

        m_lifeSlider.value = m_lifeStore;
        m_lifeUIText.text = Mathf.Round(m_lifeStore).ToString();
        m_lifeSlider.maxValue = m_lifeCapacity;

        m_hellSlider.value = m_hellStore;
        m_hellUIText.text = Mathf.Round(m_hellStore).ToString();
        m_hellSlider.maxValue = m_hellCapacity;

        m_cultValueSlider.value = m_cultSacrificeValue;
        m_cultValueSlider.maxValue = m_maxCultValue;
        m_cultValueUIText.text = Mathf.Round(m_cultSacrificeValue).ToString();
        SetValueToString(m_MaxCultValueUIText,m_maxCultValue);


        while (m_generateMana)
        {
            GenerateMana();
            UpdateManaUI();
            yield return new WaitForSeconds(m_genDelay);
        }
        
    }

    public void GenerateMana()
    {
        if (m_necroCapacity > m_necroStore)
        {
            NecroStore += m_totalNecroModifier;
        }
        else
        {
            m_necroStore = m_necroCapacity;
        }
        if (m_hellCapacity > m_hellStore)
        {
            HellStore += m_totalHellModifier;
        }
        else
        {
            m_hellStore = m_hellCapacity;
        }
        if (m_lifeCapacity > m_lifeStore)
        {
            LifeStore += m_totalLifeModifier;
        }
        else
        {
            m_lifeStore = m_lifeCapacity;
        }
    }

    void UpdateManaUI()
    {
        m_necroSlider.value = m_necroStore;
        SetValueToString(m_necroUIText, ChangeUINumber((int)m_necroStore));
        m_lifeSlider.value = m_lifeStore;
        SetValueToString(m_lifeUIText, ChangeUINumber((int)m_lifeStore)); ;
        m_hellSlider.value = m_hellStore;
        SetValueToString(m_hellUIText, ChangeUINumber((int)m_hellStore));
    }
    void UpdateCapacity()
    {
        m_necroSlider.maxValue = m_necroCapacity;
        m_lifeSlider.maxValue = m_lifeCapacity;
        m_hellSlider.maxValue = m_hellCapacity;
    }

    void UpdateGeneralUI()
    {
        m_cultValueSlider.value = m_cultSacrificeValue;
        m_cultValueUIText.text = Mathf.Round(m_cultSacrificeValue).ToString();

        m_coinValueUIText.text = Mathf.Round(m_coinCount).ToString();
        m_gemValueUIText.text = Mathf.Round(m_gemCount).ToString();
    }

    int ChangeUINumber(int value)
    {
        if (value >= 1000)
        {
            value /= 1000;
        }

        return value;
    }
    void SetValueToString(Text uiText,int value)
    {
        if(ChangeUINumber(value) <=10)
        {
            uiText.text = ChangeUINumber(value).ToString() + "K";
        }
        else
        {
            uiText.text = ChangeUINumber(value).ToString();
        }

    }
}
