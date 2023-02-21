using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMerge : MonoBehaviour
{
    private PawnMerge m_onTilePawn;
    [SerializeField] private PawnDefinitions.MPawnObjects m_objectType;
    [SerializeField] private PawnDefinitions.MMinionType m_minionType;
    [SerializeField] private PawnDefinitions.MManaType m_manaType;
    [SerializeField] private PawnDefinitions.MBuildingType m_buildingType;
    [SerializeField] private PawnDefinitions.MItemType m_itemType;
    [SerializeField] private PawnLevels m_pawnProgression;
    [SerializeField] private int m_currentLevel;
    private PawnDefinitions m_pawn;
    [SerializeField]private GameObject m_currentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        DefinePawn();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        ChangeMinionVisual();
    }
    private void ChangeMinionVisual()
    {
        Destroy(m_currentGameObject);
        m_currentGameObject = Instantiate(m_pawn.m_pawnLevels.m_pawnProgression[m_pawn.m_currentLevel],transform );
        m_currentGameObject.transform.SetParent(transform);
    }
    private void Merge(PawnDefinitions inHand,PawnMerge onTilePawn)
    {
        m_onTilePawn = onTilePawn;
        bool beingMoved = m_onTilePawn.GetComponent<PawnMovement>().GetBeingMoved();
        if (!beingMoved && inHand.m_minionType == onTilePawn.m_minionType && inHand.m_currentLevel == onTilePawn.m_currentLevel )
        {
            if (m_currentLevel < m_pawnProgression.m_pawnProgression.Length-1)
            {
                m_pawn.m_currentLevel += 1;
                m_currentLevel = m_pawn.m_currentLevel;
                
                ChangeMinionVisual();
                gameObject.GetComponent<PawnMovement>().SetHomeTile();
                gameObject.GetComponent<PawnMovement>().SetHomeTile(m_onTilePawn.GetComponent<PawnMovement>().GetHomeTile(),m_onTilePawn.GetComponent<PawnMovement>().GetHomeTileNum());
            
                Destroy(m_onTilePawn.gameObject); //Delete land minion
            }

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
