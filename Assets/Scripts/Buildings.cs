using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

struct MSpawnType
{
    public enum BuildingType {Grave,Slime,PLACEHOLDER}
}


public class Buildings : MonoBehaviour
{
    [SerializeField] private MSpawnType.BuildingType m_spawnType;
    [SerializeField] private bool m_spawnMinion = false;
    [SerializeField] private MinionDefinions.MMinionType m_minionType;
    [SerializeField] private MinionLevels[] m_buildingItems = new MinionLevels[2] ;
    [SerializeField] private GameObject m_minionPrefab;
    private GameObject m_minionObject;
    
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private GameObject[] m_gridRef;
    private int m_sizeOfGrid;

    // Start is called before the first frame update
    void Start()
    {
        //auto assign item based on type?
        
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        m_sizeOfGrid = m_GridManager.m_grid.Length;
        m_gridRef = new GameObject[m_sizeOfGrid];
        for (int i = 0; i < m_sizeOfGrid; i++)
        {
            m_gridRef[i] = m_GridManager.m_grid[i];
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (m_spawnMinion)
        {
            SpawnMinion();
            m_spawnMinion = false;
        }
    }

    public void Tapped()
    {

        SpawnMinion();
    }
    private int GenRandomNum()
    {
        return (Random.Range(0, 2));
    }
    private void SpawnMinion()
    {
        int tileNum = Random.Range(0, m_sizeOfGrid);
        m_minionObject = Instantiate(m_minionPrefab);
        m_minionObject.transform.position = new Vector3(m_gridRef[tileNum].transform.position.x,m_minionPrefab.transform.position.y,m_gridRef[tileNum].transform.position.z);
        m_gridRef[tileNum].GetComponent<TileInfo>().m_tileTaken = true;
        
        SetMinionType();
        m_spawnMinion = false;
        m_minionObject = null;
    }

    private void SetMinionType()
    {
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
    }
}
