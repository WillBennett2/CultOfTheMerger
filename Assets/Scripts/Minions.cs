
using System.Linq;
using UnityEngine;

public class Minions : MonoBehaviour
{


    [SerializeField] private MinionDefinions.MMinionType m_minionType;
    [SerializeField] private MinionLevels m_minionProgression;
    [SerializeField] private int m_currentLevel;
    private MinionDefinions m_minion;
    [SerializeField]private GameObject m_currentGameObject;
    
    [SerializeField] private GameObject m_homeTile;
    [SerializeField] private GridManager m_GridManager;

    public void SetMinionValues(MinionDefinions.MMinionType minionType,MinionLevels minionProgression, int currentLevel)
    {
        m_minionType = minionType;
        m_minionProgression = minionProgression;
        m_currentLevel = currentLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        DefineMinion();
    }
    
    private void DefineMinion()
    {
        if(m_minionProgression)
            m_minion = new MinionDefinions(m_minionType,m_minionProgression,m_currentLevel);
        m_currentGameObject = transform.GetChild(0).gameObject;
        ChangeMinionGameObject();
    }
    
    public void BeingHeld()
    {
        Debug.Log("I'm being held");
        //m_GridManager.UpdateTile(m_homeTile, false); //doesnt work in this class for some reason
        m_homeTile.GetComponent<TileInfo>().m_tileTaken = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public void Dropped()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-Vector3.up, out hit))
        {
            if (hit.transform.GetComponent<TileInfo>()!= null && !hit.transform.GetComponent<TileInfo>().m_tileTaken)        //is tile below open
            {
                Debug.Log("I was dropped");
                SetHomeTile( hit.transform.gameObject);
                //m_GridManager.UpdateTile(m_homeTile,true); //doesnt work in this class for some reason
                m_homeTile.GetComponent<TileInfo>().m_tileTaken = true;
            }
        }

        transform.position = new Vector3(m_homeTile.transform.position.x,transform.position.y,m_homeTile.transform.position.z); //snapping back to grid
        //Move current grid
    }

    public void SetHomeTile(GameObject newHomeTile)
    {
        m_homeTile = newHomeTile;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        var script = other.gameObject.GetComponent<Minions>();
        if (script != null)
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
                if (m_currentLevel >= m_minionProgression.m_minionProgression.Count())
                {
                    Debug.Log("At / Above level cap");
                }
                

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
