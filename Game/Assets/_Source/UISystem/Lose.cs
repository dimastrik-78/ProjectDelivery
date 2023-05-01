using System.Threading;
using TimerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class Lose : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
           



        }

        public void Reload()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        public void GoToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        
    }
}
