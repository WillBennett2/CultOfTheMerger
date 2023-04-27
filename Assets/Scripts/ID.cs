using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID : MonoBehaviour
{
    [SerializeField] private string m_id;
    public string GetID
    {
        get => m_id;
    }
    private void Awake()
    {
        m_id = Guid.NewGuid().ToString();
    }
}
