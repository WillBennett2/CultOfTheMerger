using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnLevels",menuName = "ScriptableObjects/PawnLevels")]
public class PawnLevels : ScriptableObject
{
    public GameObject[] m_pawnProgression = new GameObject[4];

    public GameObject[] GetPawnProgression()
    {
        return m_pawnProgression;
            
    }
}
