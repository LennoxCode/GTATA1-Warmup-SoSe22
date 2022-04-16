using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private GameObject VictoryScreen;

    [SerializeField]private GameObject looseScreen;

    [SerializeField] private GameObject victoryScreen;

    public Action onRestart;
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
        victoryScreen.SetActive(true);
    }

    public void ShowLooseScreen()
    {
        looseScreen.SetActive(true);
    }

    public void OnRestartPressed()
    {
        victoryScreen.SetActive(false);
        looseScreen.SetActive(false);
        onRestart.Invoke();
    }
    
}
