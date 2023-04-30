using System;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class PlayerInvoker : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask ground;
        [SerializeField] private LayerMask wall;
        [SerializeField] private LayerMask door;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private GameObject startRayPos;
        
        private bool _jumpOn;
        private PlayerInput _playerInput;
        private Movement _movement;

        private void Awake()
        {
            Init();
        }

        private void FixedUpdate()
        {
            _jumpOn = Physics2D.Raycast(startRayPos.transform.position, -Vector2.up, 0f,ground);
        }

        private void Update()
        {
            _movement.Move(_playerInput.Action.Movement.ReadValue<float>());
            if (_jumpOn)
            {
                _playerInput.Action.Jump.Enable();
            }
        }

        void OnEnable()
        {
            _playerInput.Enable();
        }
        
        void OnDisable()
        {
            _playerInput.Disable();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (wall.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Enable();
                rb.gravityScale = 0;
            }
        }
    
        private void OnCollisionExit2D(Collision2D other)
        {
            if (ground.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
            }
            if (wall.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
                rb.gravityScale = 1;
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (door.Contains(other.gameObject.layer))
            {
                Debug.Log("yes");
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (door.Contains(other.gameObject.layer))
            {
                Debug.Log("no");
            }
        }
    
        private void Init()
        {
            _playerInput = new PlayerInput();
            _movement = new Movement(rb, speed, jumpForce);
            _playerInput.Action.Jump.performed += context => _movement.Jump();
        }
    }
}
