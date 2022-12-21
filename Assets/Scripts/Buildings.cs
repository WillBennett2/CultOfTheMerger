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
    [SerializeField] private MinionLevels m_buildingItems;
    [SerializeField] private MinionDefinions.MMinionType m_minionType;
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

    private void SpawnMinion()
    {
        m_minionObject = Instantiate(m_minionPrefab);
        if (m_minionObject.GetComponent<Minions>())
        {
            m_minionObject.GetComponent<Minions>().SetMinionValues(m_minionType,m_buildingItems,0);
        }
        m_spawnMinion = false;
        m_minionObject = null;
    }
}
