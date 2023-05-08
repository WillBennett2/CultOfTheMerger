using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterSacrifice : MonoBehaviour
{
    private Inventory m_inventoryScript;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.m_current.onPawnSacrifice += Sacrificed;
        m_inventoryScript = FindObjectOfType<Inventory>();
    }
    
    void Sacrificed(GameObject pawnReference,PawnDefinitions.MSacrificeTypes sacrificeTypes ,float sacrificeValue)
    {
        switch (sacrificeTypes)
        {
            case PawnDefinitions.MSacrificeTypes.CultValue:
                m_inventoryScript.SacrificeValue += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.Coin:
                m_inventoryScript.Coins += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.Gem:
                m_inventoryScript.Gems += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.Necro:
                m_inventoryScript.NecroStore += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.Life:
                m_inventoryScript.LifeStore += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.Hell:
                m_inventoryScript.HellStore += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.DeathRune:
                m_inventoryScript.DeathRune += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.LifeRune:
                m_inventoryScript.LifeRune += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.HellRune:
                m_inventoryScript.HellRune += Mathf.RoundToInt(sacrificeValue);
                break;
            case PawnDefinitions.MSacrificeTypes.Rune4:
                break;
            default:
                break;
        }
        pawnReference.GetComponent<PawnMovement>().ClearHomeTile();
        pawnReference.GetComponent<PawnMovement>().GetHomeTileNum = -1;
        Destroy(pawnReference);
    }
}
