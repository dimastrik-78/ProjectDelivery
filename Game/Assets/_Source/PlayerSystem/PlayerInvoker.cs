using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class PlayerInvoker : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask ground;
        [SerializeField] private LayerMask wall;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float num;
        [SerializeField] private GameObject startRayPos;
      

        private bool jumpOn;
        private PlayerInput _playerInput;
        private Movement _movement;

        private void Awake()
        {
            Init();
        }

        private void FixedUpdate()
        {
            if(Physics2D.Raycast(startRayPos.transform.position, -Vector2.up, 0f,ground))
            {
                jumpOn = true;
            
                
            }
            else
            {
                jumpOn = false;
            }
            
        }

        private void Update()
        {
            _movement.Move(_playerInput.Action.Movement.ReadValue<float>());
            if (jumpOn)
            {
                _playerInput.Action.Jump.Enable();
            }
            else
            {
                return;
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

        //private void OnCollisionEnter2D(Collision2D other)
       // {
         //   if (ground.Contains(other.gameObject.layer))
        //    {
         //       _playerInput.Action.Jump.Enable();
          //  }
            
            
        
        private void OnCollisionStay2D(Collision2D other)
        {
            
            if (wall.Contains(other.gameObject.layer))
            {
                  
                _playerInput.Action.Jump.Enable();
                rb.velocity *=  num;




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
