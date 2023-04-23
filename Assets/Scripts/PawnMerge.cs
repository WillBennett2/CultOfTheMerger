using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMerge : MonoBehaviour
{
    [SerializeField]private PawnMovement m_thisMovementScript;
    [SerializeField] private Minions m_thisMinionScript;
    [SerializeField] private ID m_idScript;
    
    private PawnMerge m_onTilePawn;
    [SerializeField] private string m_id;
    [SerializeField] private PawnDefinitions.MPawnObjects m_objectType;
    [SerializeField] private PawnDefinitions.MMinionType m_minionType;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private PawnDefinitions.MBuildingType m_buildingType;
    [SerializeField] private PawnDefinitions.MItemType m_itemType;
    [SerializeField] private PawnDefinitions.MSacrificeTypes m_sacrificeTypes;
    [SerializeField] private PawnDefinitions.MEnemyTypes m_enemyTypes;
    [SerializeField] private PawnDefinitions.MRewardType m_rewardType;
    [SerializeField] private PawnLevels m_pawnProgression;
    [SerializeField] private int m_currentLevel;
    private PawnDefinitions m_thisPawn;
    [SerializeField]private GameObject m_currentGameObject;

    public PawnMovement GetThisMovementScript
    {
        get
        {
            return m_thisMovementScript;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(1));

        DefinePawn();
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_idScript = GetComponent<ID>();
        m_id = m_idScript.GetID;
    }

    public void SetPawnValues(PawnDefinitions.MPawnObjects objectType,PawnDefinitions.MMinionType minionType,
        PawnDefinitions.MManaType manaType,PawnDefinitions.MBuildingType buildingType,PawnDefinitions.MItemType itemType,
        PawnDefinitions.MSacrificeTypes sacrificeTypes, PawnDefinitions.MEnemyTypes enemyTypes, PawnDefinitions.MRewardType rewardType
        ,PawnLevels pawnProgression, int currentLevel)
    {
        m_objectType = objectType;
        m_minionType = minionType;
        m_manaType = manaType;
        m_buildingType = buildingType;
        m_itemType = itemType;
        m_sacrificeTypes = sacrificeTypes;
        m_pawnProgression = pawnProgression;
        m_enemyTypes = enemyTypes;
        m_rewardType = rewardType;
        m_currentLevel = currentLevel;
    }
    private void DefinePawn()
    {
        if(m_pawnProgression)
            m_thisPawn = new PawnDefinitions(m_objectType,m_minionType,m_manaType,m_buildingType,m_itemType,m_sacrificeTypes,m_enemyTypes,m_rewardType,m_pawnProgression,m_currentLevel);
        m_currentGameObject = transform.GetChild(0).gameObject;
        ChangePawnVisual();
    }
    private void ChangePawnVisual()
    {
        Destroy(m_currentGameObject);
        m_currentGameObject = Instantiate(m_thisPawn.m_pawnLevels.m_pawnProgression[m_thisPawn.m_currentLevel],transform );
        m_currentGameObject.transform.SetParent(transform);
    }
    private void Merge(PawnMerge onTilePawn)
    {
        bool sameType = false;
        m_onTilePawn = onTilePawn;
        if (m_thisPawn.m_minionType == onTilePawn.m_minionType)
        {
            sameType = true;
        }

        if (m_thisPawn.m_buildingType != PawnDefinitions.MBuildingType.Empty && m_thisPawn.m_buildingType == onTilePawn.m_buildingType)
        {
            sameType = true;
        }

        if (m_thisPawn.m_itemType != PawnDefinitions.MItemType.Empty && m_thisPawn.m_itemType == onTilePawn.m_itemType)
        {
            sameType = true;
        }
        if (onTilePawn.m_enemyTypes != PawnDefinitions.MEnemyTypes.Empty && m_thisPawn.m_minionType != PawnDefinitions.MMinionType.Empty )
        {
            onTilePawn.GetComponent<PawnEnemy>().TakeDamage(GetComponent<Minions>().Damage);
            Destroy(gameObject);
        }
        
        bool beingMoved = m_onTilePawn.GetThisMovementScript.GetBeingMoved();
        
        if (sameType && !beingMoved && m_thisPawn.m_currentLevel == onTilePawn.m_currentLevel && m_currentLevel < m_pawnProgression.m_pawnProgression.Length-1)
        {

            onTilePawn.m_thisPawn.m_currentLevel += 1;
            onTilePawn.m_currentLevel = onTilePawn.m_thisPawn.m_currentLevel;
            onTilePawn.ChangePawnVisual();

            GameEvents.m_current.PawnLevelUp(onTilePawn.m_id);
            Destroy(gameObject); // merge completed
        }
    }
    public void AttemptDropMerge()
    {
        if (m_onTilePawn) //try to merge
        {
            Merge(m_onTilePawn);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var script = other.GetComponent<PawnMerge>();
        if (script)
        {
            m_onTilePawn = script;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_onTilePawn = null;
    }
}
