using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pawn : MonoBehaviour
{
    [Header("Self Data")]
    [SerializeField] private PawnLevels m_pawnProgression;
    [SerializeField] private int m_currentLevel;
    private MinionDefinions m_minion;
    [SerializeField]private GameObject m_currentGameObject;
    
    [Header("Movement Data")]
    [SerializeField] private GameObject m_homeTile;
    [SerializeField] private int m_homeTileNum;
    [SerializeField] private GridManager m_GridManager;
    [SerializeField] private bool m_beingMoved = false;
    [FormerlySerializedAs("m_onTileMinion")] [SerializeField] private Minions m_onTilePawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            m_onTilePawn = script;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_onTilePawn = null;
    }

}
