using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is used for all the menu stuff. it once again uses the singleton pattern
/// to avoid unnecessary object searches it just hold references to the screen objects
/// and shows them on demand. there is also an action if the restart button is pressed
/// </summary>
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
