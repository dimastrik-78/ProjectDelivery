using System.Collections;
using DG.Tweening;
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
        private readonly float _climbTime;
        private readonly int _slide = Animator.StringToHash("slide");
        private readonly int _isCliming = Animator.StringToHash("isCliming");
        
        private bool _isSliding;

        public bool IsSliding => _isSliding;

        public PlayerMovement(Rigidbody2D rigidbody2D, CapsuleCollider2D collider2D, 
            float speed, float jumpForce, float slideForce, float slideTime, float climbTime)
        {
            _rigidbody2D = rigidbody2D;
            _collider2D = collider2D;
            _speed = speed;
            _jumpForce = jumpForce;
            _slideForce = slideForce;
            _slideTime = slideTime;
            _climbTime = climbTime;
        }
        
        public void Move(float value) =>
            _rigidbody2D.velocity = new Vector2(value * _speed, _rigidbody2D.velocity.y);
        

        public void Jump() =>
            _rigidbody2D.velocity = Vector2.up * _jumpForce;

        public IEnumerator Slide(Animator animator, bool cant)
        {
            if (_collider2D.size.y != 1
                && _rigidbody2D.velocity != Vector2.zero
                && cant
                && !_isSliding)
            {
                animator.SetBool(_slide, true);
                
                _isSliding = true;
                _collider2D.size = new Vector2(1.25f, 1.25f);
                _rigidbody2D.AddForce(_slideForce * _rigidbody2D.velocity, ForceMode2D.Impulse);

                yield return new WaitForSeconds(_slideTime);
                _collider2D.size = new Vector2(1.25f, 3.5f);
                animator.SetBool(_slide, false);
                _isSliding = false;
            }
        }

        public void Climb(Animator animator, Transform player, Transform endPoint, bool can)
        {
            if (can)
            {
                animator.SetBool(_isCliming, true);
                player.DOMove(endPoint.position, _climbTime, false).OnComplete(() =>
                {
                    animator.SetBool(_isCliming, false);
                });
            }
        }
    }
}