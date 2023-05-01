using System;
using UnityEngine;
using Utils;

namespace UISystem
{
    public class HeroThoughts : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                panel.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                panel.SetActive(false);
            }
        }
    }
}