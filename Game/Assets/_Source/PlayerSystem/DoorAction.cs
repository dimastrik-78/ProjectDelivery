using UnityEngine;

namespace PlayerSystem
{
    public class DoorAction : MonoBehaviour
    {
        [SerializeField] private LayerMask _player;
        [SerializeField] private float _radius;

        void Update()
        {
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _player);
            if (overlapColliders.Length > 0)
            {
                Debug.Log("Colide");
            }
        }
    }
}
