using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Inventory m_inventoryScript;
    [SerializeField] private GooglePlayManager m_googlePlayManager;
    [SerializeField] private AudioManager m_audioManager;
    [SerializeField] private Buildings m_coinChestSpawner;
    [SerializeField] public DateTime m_timeGiftWasClaimed;

    [SerializeField] private DateTime m_currentDate;
    [SerializeField] private DateTime m_previousDate;

    public void ClaimDailyGift()
    {
        bool canSpawn = false;
        m_currentDate = DateTime.Now;
        if (PlayerPrefs.HasKey("TimeGiftWasClaimed"))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString("TimeGiftWasClaimed"));
            m_previousDate = DateTime.FromBinary(temp);
        }
        else
        {
            DateTime temp = DateTime.Now;
            m_previousDate = temp;
            canSpawn = true;
        }

        TimeSpan difference = m_currentDate.Subtract(m_previousDate);

        Debug.Log(difference.Minutes);

        if (difference.Minutes>=2 || canSpawn )
        {
            Debug.Log("SPAWN CHEST");
            m_coinChestSpawner.Tapped();
            PlayerPrefs.SetString("TimeGiftWasClaimed", System.DateTime.Now.ToBinary().ToString());
            m_googlePlayManager.AchievementsTest(GPGSIds.achievement_free_is_good);
        }

        PlayButtonSound();
    }
    public void Buy25Gems()
    {
        m_inventoryScript.Gems += 25;
        PlayButtonSound();
    }
    public void Buy100Gems()
    {
        m_inventoryScript.Gems += 100;
        PlayButtonSound();
    }
    private void PlayButtonSound()
    {
        m_audioManager.Play("ButtonClick");
    }
}
