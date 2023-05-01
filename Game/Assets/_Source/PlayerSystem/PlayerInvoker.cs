using SignalSystem;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class PlayerInvoker : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CapsuleCollider2D playerCollider;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask ground;
        [SerializeField] private LayerMask wall;
        [SerializeField] private LayerMask door;
        [SerializeField] private LayerMask obstacle;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float slideForce;
        [SerializeField] private float slideTime;
        [SerializeField] private GameObject startRayPos;

        private readonly int _jump = Animator.StringToHash("jump");
        private readonly int _isHoldingToWall = Animator.StringToHash("isHoldingToWall");
        
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerAnimation _playerAnimation;
        private bool _jumpOn;

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
            if (!_playerMovement.IsSliding)
            {
                _playerMovement.Move(_playerInput.Action.Movement.ReadValue<float>());
                if (_jumpOn)
                {
                    _playerInput.Action.Jump.Enable();
                    animator.SetBool(_jump, false);
                }
            }
            
            _playerAnimation.Update();
        }

        void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Action.Interaction.Disable();
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
                animator.SetBool(_jump, false);
                animator.SetBool(_isHoldingToWall, true);
            }

            if (obstacle.Contains(other.gameObject.layer))
            {
                Signals.Get<RemoveTimeSignal>().Dispatch();
            }
        }
    
        private void OnCollisionExit2D(Collision2D other)
        {
            if (ground.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
                animator.SetBool(_jump, true);
            }
            
            if (wall.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
                rb.gravityScale = 1;
                animator.SetBool(_jump, false);
                animator.SetBool(_isHoldingToWall, false);
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (door.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Interaction.Enable();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (door.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Interaction.Disable();
            }
        }
    
        private void Init()
        {
            _playerInput = new PlayerInput();
            _playerMovement = new PlayerMovement(rb, playerCollider, speed, jumpForce, slideForce, slideTime);
            _playerAnimation = new PlayerAnimation(rb, sprite, animator);
                
            _playerInput.Action.Jump.performed += context => _playerMovement.Jump();
            _playerInput.Action.Slide.performed += context => StartCoroutine(_playerMovement.Slide(animator));
            _playerInput.Action.Interaction.performed += context => Signals.Get<AddTimeSignal>().Dispatch();
            _playerInput.Action.Interaction.performed += context => Signals.Get<DisableDoorSignal>().Dispatch();
        }
    }
}
