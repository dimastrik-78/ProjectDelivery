using System;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimation
    {
        private readonly Rigidbody2D _rb;
        private readonly SpriteRenderer _sprite;
        private readonly Animator _animator;
        private readonly int _speedID = Animator.StringToHash("speed");

        public PlayerAnimation(Rigidbody2D rb, SpriteRenderer sprite, Animator animator)
        {
            _rb = rb;
            _sprite = sprite;
            _animator = animator;
        }

        public void Update()
        {
            _animator.SetFloat(_speedID, Math.Abs(_rb.velocity.x));

            if (_rb.velocity.x < 0)
            {
                _sprite.flipX = true;
            }
            else if (_rb.velocity.x > 0)
            {
                _sprite.flipX = false;
            }
        }
    }
}