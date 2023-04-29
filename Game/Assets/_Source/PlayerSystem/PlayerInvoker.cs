using System;
using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        
        private PlayerInput _playerInput;
        private Movement _movement;

        private void Awake()
        {
            Init();
        }

        void OnEnable()
        {
            _playerInput.Enable();

            StartCoroutine(Move());
        }
        
        void OnDisable()
        {
            _playerInput.Disable();

            StopCoroutine(Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            
            _movement.Move(_playerInput.Action.Movement.ReadValue<float>());

            StartCoroutine(Move());
        }

        private void Init()
        {
            _playerInput = new PlayerInput();
            _movement = new Movement(rb, speed, jumpForce);
            _playerInput.Action.Jump.performed += context => _movement.Jump();
        }
    }
}
