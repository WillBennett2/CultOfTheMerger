using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuildingItems",menuName = "ScriptableObjects/BuildingItems")]
public class BuildingItems : ScriptableObject
{
    public GameObject[] m_buildingItems = new GameObject[2];
}
