using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
        [SerializeField] private float speed = 3;
        Rigidbody2D _rb;
        private PlayerSlide _playerSlide;
        private float moveInput;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerSlide = GetComponent<PlayerSlide>();
        }
        
        private void FixedUpdate()
        {
            if (_playerSlide.IsSliding == false)
            {
                moveInput = Input.GetAxisRaw("Horizontal");
                _rb.velocity = new Vector2(moveInput * speed, _rb.velocity.y);
            }
        }
    
        
    
        
}
