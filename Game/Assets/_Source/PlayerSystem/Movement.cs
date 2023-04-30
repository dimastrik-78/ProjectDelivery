using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _speed;
        private readonly float _jumpForce;

        public Movement(Rigidbody2D rigidbody2D, float speed, float jumpForce)
        {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
            _jumpForce = jumpForce;
        }
        
        public void Move(float value)
        {
            _rigidbody2D.velocity = new Vector2(value * _speed, _rigidbody2D.velocity.y);
        }

        public void Jump()
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;

        }
        
        
        
    }
}