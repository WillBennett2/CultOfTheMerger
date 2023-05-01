using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID : MonoBehaviour
{
    [SerializeField] private string m_id;
    private PawnSacrifice m_pawnSacrificeScript;
    public string GetID
    {
        get => m_id;
    }
    private void Awake()
    {
        m_id = Guid.NewGuid().ToString();
        m_pawnSacrificeScript = GetComponent<PawnSacrifice>();
        if (m_pawnSacrificeScript)
        {
            m_pawnSacrificeScript.m_id = m_id;
        }
    }
}
