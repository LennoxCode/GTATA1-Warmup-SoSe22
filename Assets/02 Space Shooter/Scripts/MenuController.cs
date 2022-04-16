using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private GameObject VictoryScreen;

    private GameObject LooseScreen;
    // Start is called before the first frame update
    public static MenuController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void ShowVictoryScreen()
    {
        
    }

    public void ShowLooseScreen()
    {
        
    }
    
}
