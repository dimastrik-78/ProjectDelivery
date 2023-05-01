using UnityEngine;

namespace ObjectInteractionSystem
{
    public class Stairs : MonoBehaviour
    {
        [SerializeField] private Transform point;

        public Transform GetPoint => point;
    }
}
