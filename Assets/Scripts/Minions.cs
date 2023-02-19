
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
    [SerializeField] private GameObject m_homeTile;
    [SerializeField] private int m_homeTileNum;
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] public bool m_beingMoved = false;
    [SerializeField] private Minions m_onTileMinion;

    public void SetMinionValues(MinionDefinions.MMinionType minionType,MinionLevels minionProgression, int currentLevel)
    {
        m_minionType = minionType;
        m_minionProgression = minionProgression;
        m_currentLevel = currentLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_GridManager = Camera.main.GetComponent<GridManager>();
        DefineMinion();
    }
    
    private void DefineMinion()
    {
        if(m_minionProgression)
            m_minion = new MinionDefinions(m_minionType,m_minionProgression,m_currentLevel);
        m_currentGameObject = transform.GetChild(0).gameObject;
        ChangeMinionVisual();
    }
    
    public void BeingHeld()
    {
        Debug.Log("I'm being held");
        //m_GridManager.UpdateTile(m_homeTile, false); //doesnt work in this class for some reason
        m_beingMoved = true;
    }

    public void Dropped()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-Vector3.up, out hit))
        {
            if (hit.transform.GetComponent<TileInfo>()!= null)        //is tile below open
            {
                if (!hit.transform.GetComponent<TileInfo>().m_tileTaken) // take tile
                {
                    TileInfo hitTileInfo = hit.transform.GetComponent<TileInfo>();
                    m_GridManager.UpdateTile(m_homeTileNum,false);
                    GameObject newHomeTile = hit.transform.gameObject;
                    int newHomeTileNum = hitTileInfo.m_tileNum;
                    SetHomeTile(newHomeTile,newHomeTileNum );
                    m_GridManager.UpdateTile(m_homeTileNum,true);
                }
            }
            else if(m_onTileMinion!=null) //try to merge
            {
                Debug.Log("Drop merge");
                Merge(m_minion,m_onTileMinion);
            }
        }
        
        MoveToHomeTile();
        //Move current grid
        m_beingMoved = false;
    }

    public void SetHomeTile(GameObject newHomeTile,int newHomeTileNum)
    {
        m_homeTile = newHomeTile;
        m_homeTileNum = newHomeTileNum;
        MoveToHomeTile();
    }

    private void MoveToHomeTile()
    {
        transform.position = new Vector3(m_homeTile.transform.position.x,transform.position.y,m_homeTile.transform.position.z); //snapping back to grid
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision!");
        var script = other.gameObject.GetComponent<Minions>();
        if (script != null)
        {
            m_onTileMinion = script;
            /*if (!m_beingMoved)
            {
                Merge(m_minion, script.gameObject);
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_onTileMinion = null;
    }

    private void Merge(MinionDefinions inHand,Minions onTileMinion)
    {
        MinionDefinions onTile = onTileMinion.m_minion;
        bool beingMoved = onTileMinion.m_beingMoved;
        if (!beingMoved)
        {
            if (inHand.m_minionType == onTile.m_minionType)
            {
                if (inHand.m_currentLevel == onTile.m_currentLevel)
                {
                    Debug.Log("yay");
                    if (m_currentLevel < m_minionProgression.m_minionProgression.Count())
                    {
                        m_minion.m_currentLevel += 1;
                        m_currentLevel = m_minion.m_currentLevel;
                    }
                    
                    ChangeMinionVisual();
                    m_GridManager.UpdateTile(m_homeTileNum,false);
                    SetHomeTile(onTileMinion.m_homeTile,onTileMinion.m_homeTileNum);
                    
                    Destroy(onTileMinion.gameObject); //Delete land minion
                }
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
