using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerMovement
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly CapsuleCollider2D _collider2D;
        private readonly float _speed;
        private readonly float _jumpForce;
        private readonly float _slideForce;
        private readonly float _slideTime;

        public PlayerMovement(Rigidbody2D rigidbody2D, CapsuleCollider2D collider2D, float speed, float jumpForce, float slideForce, float slideTime)
        {
            _rigidbody2D = rigidbody2D;
            _collider2D = collider2D;
            _speed = speed;
            _jumpForce = jumpForce;
            _slideForce = slideForce;
            _slideTime = slideTime;
        }
        
        public void Move(float value)
        {
            _rigidbody2D.velocity = new Vector2(value * _speed, _rigidbody2D.velocity.y);
        }

        public void Jump()
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
        }

        public IEnumerator Slide()
        {
            Debug.Log("test");
            if (_collider2D.size.y != 1
                && _rigidbody2D.velocity != Vector2.zero)
            {
                // _isSliding = true;
                _collider2D.size = new Vector2(1, 1); // _slideTime

                _rigidbody2D.AddForce(_slideForce * _rigidbody2D.velocity, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(_slideTime);
            _collider2D.size = new Vector2(1, 2);
        }
    }
}