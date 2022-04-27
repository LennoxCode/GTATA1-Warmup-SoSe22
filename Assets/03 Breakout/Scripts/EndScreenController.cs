using System;
using Scripts;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

namespace Scripts
{
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

        private void OnDestroy()
        {
            BreakoutController.onGameOver -= ShowGameOver;
        }

        // Update is called once per frame
        void Update()
        {
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