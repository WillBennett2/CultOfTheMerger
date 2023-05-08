using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnEnemy : MonoBehaviour
{
    [SerializeField] private PawnDefinitions.MEnemyTypes m_enemyTypes;
    [SerializeField] private PawnDefinitions.MManaType m_manaAttraction;
    [SerializeField] private float m_health;
    [SerializeField] private string m_rewardSpawnerName;
    [SerializeField] private GameObject m_pawnLevel;
    
    private PawnDefinitions m_pawn;

    private Camera m_camera;
    [SerializeField]private Buildings m_buildingScript;

    private void Start()
    {
        m_camera = Camera.main;
        m_buildingScript = GetComponent<Buildings>(); //GameObject.Find(m_rewardSpawnerName).GetComponent<Buildings>();
    }

    public void SetEnemyValues(PawnDefinitions.MEnemyTypes enemyTypes,
        PawnDefinitions.MManaType manaAttraction,float health, string reward)
    {
        m_enemyTypes = enemyTypes;
        m_manaAttraction = manaAttraction;
        m_health = health;
        m_rewardSpawnerName = reward;
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            Death();
        }

    }

    private void Death()
    {
        Debug.Log("Ahhhhh");
        //spawn reward
        m_buildingScript.Tapped();
        Destroy(gameObject);
    }
}
