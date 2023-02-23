using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region  Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance)
            Destroy(gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(this);
        
    }
    #endregion

    public GameObject SelectedPawn
    {
        get { return m_selectedPawn; }

        set { m_selectedPawn = value; }

    }

    [SerializeField] private GameObject m_selectedPawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
