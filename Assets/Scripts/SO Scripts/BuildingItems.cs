using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuildingItems",menuName = "ScriptableObjects/BuildingItems")]
public class BuildingItems : ScriptableObject
{
    [SerializeField]private PawnLevels[] m_buildingItems = new PawnLevels[2];

    public PawnLevels GetPawnLevels(int index)
    {
        return m_buildingItems[index];
    }
}
