using SignalSystem;
using UnityEngine;
using Utils;

namespace ObjectInteractionSystem
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                Signals.Get<DisableDoorSignal>().AddListener(Disable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                Signals.Get<DisableDoorSignal>().RemoveListener(Disable);
            }
        }

        private void Disable()
        {
            boxCollider.enabled = false;
            Signals.Get<DisableDoorSignal>().RemoveListener(Disable);
        }
    }
}