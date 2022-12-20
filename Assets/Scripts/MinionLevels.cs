using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinionLevels",menuName = "ScriptableObjects/MinionLevels")]
public class MinionLevels : ScriptableObject
{
    public GameObject[] m_minionProgression = new GameObject[4];
}
