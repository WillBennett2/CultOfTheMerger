using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Serializable]public struct RuneCosts
    {
        public int m_graveRuneCost;
        public int m_lifeRuneCost;
        public int m_hellRuneCost;
        public int m_specialRuneCost;
    }
    [SerializeField]private RuneCosts m_runeCosts;
    
    public RuneCosts RuneCost
    {
        get
        {
            return m_runeCosts;
        }
    }
    
}
