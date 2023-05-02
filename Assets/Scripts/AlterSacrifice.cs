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
            case PawnDefinitions.MSacrificeTypes.Mana1:
                break;
            case PawnDefinitions.MSacrificeTypes.Mana2:
                break;
            case PawnDefinitions.MSacrificeTypes.Mana3:
                break;
            case PawnDefinitions.MSacrificeTypes.Rune1:
                break;
            case PawnDefinitions.MSacrificeTypes.Rune2:
                break;
            case PawnDefinitions.MSacrificeTypes.Rune3:
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
