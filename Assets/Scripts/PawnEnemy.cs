using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnEnemy : MonoBehaviour
{
    [SerializeField] private PawnDefinitions.MEnemyTypes m_enemyTypes;
    [SerializeField] private PawnDefinitions.MManaType m_manaAttraction;
    [SerializeField] private float m_health;
    [SerializeField] private GameObject m_reward;
    [SerializeField] private GameObject m_pawnLevel;
    
    private PawnDefinitions m_pawn;

    public void SetEnemyValues(PawnDefinitions.MEnemyTypes enemyTypes,
        PawnDefinitions.MManaType manaAttraction,float health, GameObject reward)
    {
        m_enemyTypes = enemyTypes;
        m_manaAttraction = manaAttraction;
        m_health = health;
        m_reward = reward;
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
        
        Destroy(gameObject);
    }
}
