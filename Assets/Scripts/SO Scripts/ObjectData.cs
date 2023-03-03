using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData")]
public class ObjectData : ScriptableObject
{
    [System.Serializable] struct Minion
    {
        [SerializeField]private PawnLevels m_pawnLevels;
        [SerializeField]private PawnDefinitions.MManaType m_manaType;
        [SerializeField]private float m_manaMultiplier;
        [SerializeField]private float m_sacrificialBaseValue;
        [SerializeField]private float m_sacrificialMultiplier;
    }
    
    [SerializeField] private Minion[] Objects;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}