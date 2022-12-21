using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MSpawnType
{
    public enum BuildingType {Grave,Slime,PLACEHOLDER}
}


public class Buildings : MonoBehaviour
{
    [SerializeField] private MSpawnType.BuildingType m_spawnType;
    [SerializeField] private bool m_spawnMinion;
    [SerializeField] private MinionDefinions.MMinionType m_minionType;
    [SerializeField] private MinionLevels[] m_buildingItems = new MinionLevels[2] ;
    [SerializeField] private GameObject m_minionPrefab;
    private GameObject m_minionObject;

    // Start is called before the first frame update
    void Start()
    {
        //auto assign item based on type?
    }

    // Update is called once per frame
    void Update()
    {
        if (m_spawnMinion)
        {
            SpawnMinion();
        }
    }

    private int GenRandomNum()
    {
        return (Random.Range(0, 2));
    }
    private void SpawnMinion()
    {
        m_minionObject = Instantiate(m_minionPrefab);
        Minions minionsScript = m_minionObject.GetComponent<Minions>();
        if (minionsScript)
        {
            if (GenRandomNum() == 0)
            {
                m_minionType = MinionDefinions.MMinionType.Skeleton;
            }
            else
            {
                m_minionType = MinionDefinions.MMinionType.Zombie;
            }
            if (m_minionType == MinionDefinions.MMinionType.Skeleton)
            {
                minionsScript.SetMinionValues(m_minionType, m_buildingItems[0], 0);
            }
            else
            {
                minionsScript.SetMinionValues(m_minionType, m_buildingItems[1], 0);
            }
        }
        m_spawnMinion = false;
        m_minionObject = null;
    }
}
