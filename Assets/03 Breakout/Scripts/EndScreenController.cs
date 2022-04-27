using System;
using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    /// <summary>
    /// This class is another menu controller it is subscribed to the game over event to ensure the menu is
    /// shown after the game ends and has to actions itself for restarting and advancing to the next level
    /// this time I got the idea to use static actions to make subscribing easier for classes that are only
    /// present once like this one or the BreakOutController. 
    /// </summary>
    public class EndScreenController : MonoBehaviour
    {
        // Start is called before the first frame update
        public static Action restartGame;
        public static Action loadNextLevel;
        [SerializeField] private Image backGround;
        [SerializeField] private GameObject victoryScreen;
        [SerializeField] private GameObject defeatScreen;

  

        void Start()
        {
            BreakoutController.onGameOver += ShowGameOver;
            HideMenus();
        }
        //it is important to unsubscribe from static events onDestroy or else there are nullpointer exceptions after reloading
        private void OnDestroy()
        {
            BreakoutController.onGameOver -= ShowGameOver;
        }
        
        private void ShowGameOver(bool hasWon)
        {
            backGround = GetComponent<Image>();
            backGround.enabled = true;
            if (hasWon) ShowVictoryScreen();
            else ShowDefeatScreen();
        }

        private void ShowVictoryScreen()
        {
            victoryScreen.SetActive(true);
        }

        private void HideMenus()
        {
            backGround.enabled = false;
            victoryScreen.SetActive(false);
            defeatScreen.SetActive(false);

        }

        private void ShowDefeatScreen()
        {
            defeatScreen.SetActive(true);
        }

        public void NextLevelPressed()
        {
            loadNextLevel?.Invoke();
            HideMenus();

        }

        public void RestartPressed()
        {
            restartGame?.Invoke();
            HideMenus();

        }
    }
}