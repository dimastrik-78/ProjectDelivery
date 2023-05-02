using AudioSystem;
using ObjectInteractionSystem;
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
        [SerializeField] private LayerMask climb;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float slideForce;
        [SerializeField] private float slideTime;
        [SerializeField] private float climbTime;
        [SerializeField] private GameObject startRayPos;

        private readonly int _jump = Animator.StringToHash("jump");
        private readonly int _isHoldingToWall = Animator.StringToHash("isHoldingToWall");

        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerAnimation _playerAnimation;
        private bool _jumpOn;
        private bool _canClim;
        private Transform _endPointClimb;

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
                    _playerInput.Action.Slide.Enable();
                    animator.SetBool(_jump, false);
                }
            }
            _playerAnimation.Update();
        }

        void OnEnable()
        {
            _playerInput.Enable();
            _playerInput.Action.InteractionDoor.Disable();
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
        }
    
        private void OnCollisionExit2D(Collision2D other)
        {
            if (ground.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
                _playerInput.Action.Slide.Enable();
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
                _playerInput.Action.InteractionDoor.Enable();
            }
            
            if (climb.Contains(other.gameObject.layer))
            {
                _endPointClimb = other.GetComponent<Stairs>().GetPoint;
                _playerInput.Action.InteractionClimb.Enable();
                _canClim = true;
            }
            
            if (obstacle.Contains(other.gameObject.layer))
            {
                Audio.DamageAudio.Play();
                Signals.Get<RemoveTimeSignal>().Dispatch();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (door.Contains(other.gameObject.layer))
            {
                _playerInput.Action.InteractionDoor.Disable();
            }
            
            if (climb.Contains(other.gameObject.layer))
            {
                _playerInput.Action.InteractionClimb.Disable();
                _canClim = false;
            }
        }
    
        private void Init()
        {
            _playerInput = new PlayerInput();
            _playerMovement = new PlayerMovement(rb, playerCollider, speed, jumpForce, slideForce, slideTime, climbTime);
            _playerAnimation = new PlayerAnimation(rb, sprite, animator);
                
            _playerInput.Action.Jump.performed += context => _playerMovement.Jump();
            _playerInput.Action.Jump.performed += context => Audio.JumpAudio.Play();
            _playerInput.Action.Jump.performed += context => _playerInput.Action.Slide.Disable();
            _playerInput.Action.Slide.performed += context => StartCoroutine(_playerMovement.Slide(animator, _jumpOn));
            _playerInput.Action.InteractionDoor.performed += context => Signals.Get<AddTimeSignal>().Dispatch();
            _playerInput.Action.InteractionDoor.performed += context => Signals.Get<DisableDoorSignal>().Dispatch();
            _playerInput.Action.InteractionClimb.performed += context => _playerMovement.Climb(animator, transform, _endPointClimb, _canClim);
            
            _playerInput.Action.InteractionDoor.Disable();
            _playerInput.Action.InteractionClimb.Disable();
        }
    }
}
