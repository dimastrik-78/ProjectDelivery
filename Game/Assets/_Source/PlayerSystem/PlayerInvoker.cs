using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class PlayerInvoker : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask ground;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        
        private PlayerInput _playerInput;
        private Movement _movement;

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            _movement.Move(_playerInput.Action.Movement.ReadValue<float>());
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
            if (ground.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Enable();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (ground.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
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
