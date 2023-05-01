using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class WinUI : MonoBehaviour
    {
        public void Menu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        
        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
}