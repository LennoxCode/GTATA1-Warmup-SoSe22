using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    /// <summary>
    /// this class is responsible for changing scenes and reloading.
    /// it is subscribed to two events of the menuController
    /// 
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        private int currentLevelIndex;
        //these used to be levelAssets but it gave me too much hassle when building
        [SerializeField] private String[] levels;

        // this class uses dont destroy on load so it gets carried over between scenes
        // an ensure that the state remains untouched
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
            currentLevelIndex = sceneIndex;
            SceneManager.LoadScene(levels[sceneIndex]);
        }

        public void LoadNextLevel()
        {
            if (currentLevelIndex >= levels.Length - 1) currentLevelIndex = 0;
            else currentLevelIndex++;
            SceneManager.LoadScene(levels[currentLevelIndex]);

        }

        public void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        }
    }
}