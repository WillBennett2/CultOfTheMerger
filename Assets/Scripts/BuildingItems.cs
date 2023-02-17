using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuildingItems",menuName = "ScriptableObjects/BuildingItems")]
public class BuildingItems : ScriptableObject
{
    [SerializeField]private MinionLevels[] m_buildingItems = new MinionLevels[2];

    public MinionLevels GetMinionLevels(int index)
    {

        return m_buildingItems[index];
    }
}
