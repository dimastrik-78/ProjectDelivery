using AudioSystem;
using UnityEngine;
using Utils;

namespace UISystem
{
    public class Win : MonoBehaviour
    {
        [SerializeField] private LayerMask player;
        [SerializeField] private GameObject winPanel;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                Audio.SoundtrackAudio.Stop();
                Audio.GameOverAudio.Play();
                winPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}