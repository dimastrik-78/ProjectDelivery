using SignalSystem;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class PlayerInvoker : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CapsuleCollider2D playerCollider;
        [SerializeField] private LayerMask ground;
        [SerializeField] private LayerMask wall;
        [SerializeField] private LayerMask door;
        [SerializeField] private LayerMask obstacle;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float slideForce;
        [SerializeField] private float slideTime;
        [SerializeField] private GameObject startRayPos;
        
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
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
            _playerMovement.Move(_playerInput.Action.Movement.ReadValue<float>());
            if (_jumpOn)
            {
                _playerInput.Action.Jump.Enable();
            }
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
            }
            if (wall.Contains(other.gameObject.layer))
            {
                _playerInput.Action.Jump.Disable();
                rb.gravityScale = 1;
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
            _playerInput.Action.Jump.performed += context => _playerMovement.Jump();
            _playerInput.Action.Slide.performed += context => StartCoroutine(_playerMovement.Slide());
            _playerInput.Action.Interaction.performed += context => Signals.Get<AddTimeSignal>().Dispatch();
            _playerInput.Action.Interaction.performed += context => Signals.Get<DisableDoorSignal>().Dispatch();
        }
    }
}
