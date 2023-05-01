using System;
using SignalSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class Lose : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        private void Awake()
        {
            Signals.Get<GameOverSignal>().AddListener(GameOver);
        }

        private void GameOver()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Signals.Get<GameOverSignal>().RemoveListener(GameOver);
        }

        public void Reload()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
        
        public void GoToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        
    }
}
