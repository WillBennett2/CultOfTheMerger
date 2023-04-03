using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMerge : MonoBehaviour
{
    [SerializeField]private PawnMovement m_thisMovementScript;
    [SerializeField] private Minions m_thisMinionScript;
    
    private PawnMerge m_onTilePawn;
    [SerializeField] private int m_id;
    [SerializeField] private PawnDefinitions.MPawnObjects m_objectType;
    [SerializeField] private PawnDefinitions.MMinionType m_minionType;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private PawnDefinitions.MBuildingType m_buildingType;
    [SerializeField] private PawnDefinitions.MItemType m_itemType;
    [SerializeField] private PawnLevels m_pawnProgression;
    [SerializeField] private int m_currentLevel;
    private PawnDefinitions m_pawn;
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
        if (gameObject.GetComponent<Minions>())
        {
            m_id = m_thisMinionScript.ID;   
        }
        
        DefinePawn();
    }
    
    public void SetPawnValues(PawnDefinitions.MPawnObjects objectType,PawnDefinitions.MMinionType minionType,
        PawnDefinitions.MManaType manaType,PawnDefinitions.MBuildingType buildingType,PawnDefinitions.MItemType itemType,
        PawnLevels pawnProgression, int currentLevel)
    {
        m_objectType = objectType;
        m_minionType = minionType;
        m_manaType = manaType;
        m_buildingType = buildingType;
        m_itemType = itemType;
        m_pawnProgression = pawnProgression;
        m_currentLevel = currentLevel;
    }
    private void DefinePawn()
    {
        if(m_pawnProgression)
            m_pawn = new PawnDefinitions(m_objectType,m_minionType,m_manaType,m_buildingType,m_itemType,m_pawnProgression,m_currentLevel);
        m_currentGameObject = transform.GetChild(0).gameObject;
        ChangePawnVisual();
    }
    private void ChangePawnVisual()
    {
        Destroy(m_currentGameObject);
        m_currentGameObject = Instantiate(m_pawn.m_pawnLevels.m_pawnProgression[m_pawn.m_currentLevel],transform );
        m_currentGameObject.transform.SetParent(transform);
    }
    private void Merge(PawnDefinitions inHand,PawnMerge onTilePawn)
    {
        bool sameType = false;
        m_onTilePawn = onTilePawn;
        if (inHand.m_minionType == onTilePawn.m_minionType)
        {
            sameType = true;
        }

        if (inHand.m_buildingType != PawnDefinitions.MBuildingType.Empty && inHand.m_buildingType == onTilePawn.m_buildingType)
        {
            sameType = true;
        }

        if (inHand.m_itemType != PawnDefinitions.MItemType.Empty && inHand.m_itemType == onTilePawn.m_itemType)
        {
            sameType = true;
        }
        
        bool beingMoved = m_onTilePawn.GetThisMovementScript.GetBeingMoved();
        if (sameType && !beingMoved && inHand.m_currentLevel == onTilePawn.m_currentLevel && m_currentLevel < m_pawnProgression.m_pawnProgression.Length-1)
        {
            m_pawn.m_currentLevel += 1;
            m_currentLevel = m_pawn.m_currentLevel;
            
            ChangePawnVisual();
            m_thisMovementScript.SetHomeTile();
            m_thisMovementScript.SetHomeTile(m_onTilePawn.GetThisMovementScript.GetHomeTile(),m_onTilePawn.GetThisMovementScript.GetHomeTileNum());
        
            Destroy(m_onTilePawn.gameObject); //Delete land minion

            GameEvents.m_current.MinionLevelUp(m_id);
        }
    }
    public void AttemptDropMerge()
    {
        if (m_onTilePawn!=null) //try to merge
        {
            Merge(m_pawn,m_onTilePawn);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var script = other.gameObject.GetComponent<PawnMerge>();
        if (script != null)
        {
            m_onTilePawn = script;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_onTilePawn = null;
    }
}
