using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents m_current;

    public void Awake()
    {
        m_current = this;
    }
    public event Action<int> onMinionLevelUp;
    public void MinionLevelUp(int id)
    {
        if (onMinionLevelUp!=null)
        {
            onMinionLevelUp(id);
        }
    }

    public event Action<GameObject, PawnDefinitions.MSacrificeTypes,float> onPawnSacrifice;

    public void PawnSacrifice(GameObject pawnReference,PawnDefinitions.MSacrificeTypes sacrificeTypes ,float sacrificeValue)
    {
        if (onPawnSacrifice!=null)
        {
            onPawnSacrifice(pawnReference,sacrificeTypes ,sacrificeValue);
        }
    }
}
