using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Minions : MonoBehaviour
{


    [SerializeField] private MinionDefinions.MMinionType m_minionType;
    [SerializeField] private MinionLevels m_minionProgression;
    [SerializeField] private int m_currentLevel;
    private MinionDefinions m_minion;
    [SerializeField]private GameObject m_currentGameObject;


    [SerializeField] public Vector3 m_previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        //m_minionManager = GameObject.FindGameObjectWithTag("MinionManager").GetComponent<MinionManagers>();
        if(m_minionProgression!=null)
            m_minion = new MinionDefinions(m_minionType,m_minionProgression,m_currentLevel);
        m_currentGameObject = transform.GetChild(0).gameObject;
        ChangeMinionGameObject();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        var script = other.gameObject.GetComponent<Minions>();
        if (script!=null)
        {
            Merge(m_minion, script.gameObject);
        }
    }

    private void Merge(MinionDefinions inHand,GameObject onTileGameObject)
    {
        MinionDefinions onTile = onTileGameObject.GetComponent<Minions>().m_minion;   
        if (inHand.m_minionType == onTile.m_minionType)
        {
            if (inHand.m_currentLevel == onTile.m_currentLevel)
            {
                Debug.Log("yay");

                m_minion.m_currentLevel += 1;
                m_currentLevel = m_minion.m_currentLevel;

                ChangeMinionGameObject();
                Destroy(onTileGameObject);                //Delete land minion
            }
        }
    }

    private void ChangeMinionGameObject()
    {
        Destroy(m_currentGameObject);
        m_currentGameObject = Instantiate(m_minion.m_minionLevels.m_minionProgression[m_minion.m_currentLevel],transform );
        m_currentGameObject.transform.SetParent(transform);
    }
}
