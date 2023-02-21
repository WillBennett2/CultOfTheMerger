
using System;
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{
    [Header("Self Data")]
    [SerializeField] private MinionDefinions.MMinionType m_minionType;
    [SerializeField] private MinionLevels m_minionProgression;
    [SerializeField] private int m_currentLevel;
    private MinionDefinions m_minion;
    [SerializeField]private GameObject m_currentGameObject;

    [Header("Movement Data")] 
    private PawnMovement m_pawnMovement;
    [SerializeField] private Minions m_onTileMinion;

    public void SetMinionValues(MinionDefinions.MMinionType minionType,MinionLevels minionProgression, int currentLevel)
    {
        m_minionType = minionType;
        m_minionProgression = minionProgression;
        m_currentLevel = currentLevel;
    }
    void Start()
    {
        m_pawnMovement=gameObject.GetComponent<PawnMovement>();
        DefineMinion();
    }
    
    private void DefineMinion()
    {
        if(m_minionProgression)
            m_minion = new MinionDefinions(m_minionType,m_minionProgression,m_currentLevel);
        m_currentGameObject = transform.GetChild(0).gameObject;
        ChangeMinionVisual();
    }

    public void AttemptDropMerge()
    {
        if (m_onTileMinion!=null) //try to merge
        {
            Debug.Log("Drop merge");
            Merge(m_minion,m_onTileMinion);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision!");
        var script = other.gameObject.GetComponent<Minions>();
        if (script != null)
        {
            m_onTileMinion = script;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_onTileMinion = null;
    }

    private void Merge(MinionDefinions inHand,Minions onTileMinion)
    {
        MinionDefinions onTile = onTileMinion.m_minion;
        bool beingMoved = onTileMinion.GetComponent<PawnMovement>().GetBeingMoved();
        if (!beingMoved && inHand.m_minionType == onTile.m_minionType && inHand.m_currentLevel == onTile.m_currentLevel )
        {
            Debug.Log("yay");
            if (m_currentLevel < m_minionProgression.m_minionProgression.Count()-1)
            {
                m_minion.m_currentLevel += 1;
                m_currentLevel = m_minion.m_currentLevel;
                
                ChangeMinionVisual();
                m_pawnMovement.SetHomeTile();
                m_pawnMovement.SetHomeTile(onTileMinion.GetComponent<PawnMovement>().GetHomeTile(),onTileMinion.GetComponent<PawnMovement>().GetHomeTileNum());
            
                Destroy(onTileMinion.gameObject); //Delete land minion
            }

        }
    }

    private void ChangeMinionVisual()
    {
        Destroy(m_currentGameObject);
        m_currentGameObject = Instantiate(m_minion.m_minionLevels.m_minionProgression[m_minion.m_currentLevel],transform );
        m_currentGameObject.transform.SetParent(transform);
    }
}
