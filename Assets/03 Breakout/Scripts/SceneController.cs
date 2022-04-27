using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    private int currentLevelIndex;
    [SerializeField] private SceneAsset[] levels;
    
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EndScreenController.restartGame -= ReloadCurrentLevel;
        EndScreenController.restartGame += ReloadCurrentLevel;
        EndScreenController.loadNextLevel -= LoadNextLevel;
        EndScreenController.loadNextLevel += LoadNextLevel;
    }

    public void LoadLevel(int sceneIndex)
    {
       SceneManager.LoadScene(levels[sceneIndex].name);
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex == levels.Length) return;
        currentLevelIndex++;
        SceneManager.LoadScene(levels[currentLevelIndex].name);
        
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
}
