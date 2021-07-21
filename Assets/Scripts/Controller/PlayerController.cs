using UnityEngine;
using UnityEngine.InputSystem;
using FSMOverview.Scripts.InputActions;

namespace FSMOverview.Scripts.Controller
{
    public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        [SerializeField]
        private CharacterController _controller;

        private Vector2 _moveInputContext;
        private float _horizontalInput;

        private bool _jumpInput;

        private Vector3 _direction,
            _velocity;

        [SerializeField]
        private float _speed = 5f,
            _jumpHeight = 2f;

        private float _gravity = -9.81f,
            _yVelocity = 0f;

        // Start is called before the first frame update
        void Start()
        {
            if (_controller == null)
            {
                if (!TryGetComponent(out _controller))
                {
                    Debug.Log("PlayerController::Start()::CharacterController is NULL");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            CalculateMovement(_horizontalInput);
        }

        // Move action from PlayerInputActions
        public void OnMove(InputAction.CallbackContext context)
        {
            // Reads value from input as Vector2
            _moveInputContext = context.ReadValue<Vector2>();

            // Sets float value for horizontal movement
            _horizontalInput = _moveInputContext.x;  
        }

        private void CalculateMovement(float horizontalInput)
        {
            // Check to see if the controller is grounded
            // If so, set movement direction and velocity
            // If not, apply gravity to Y velocity
            // Move controller based on velocity
            if (_controller.isGrounded)
            {
                _direction = Vector3.right * horizontalInput;
                _velocity = _direction * _speed;

                // Check for jump input only if the player is grounded
                if (_jumpInput)
                {
                    CalculateJump();
                }
            }
            else
            {
                _yVelocity += _gravity * Time.deltaTime;
            }

            _velocity.y = _yVelocity;

            _controller.Move(_velocity * Time.deltaTime);
        }

        // Jump action from PlayerInputActions
        public void OnJump(InputAction.CallbackContext context)
        {
            // Reads value from input as bool to determine if the action was performed
            _jumpInput = context.ReadValue<float>() == 1;  
        }

        private void CalculateJump()
        {
            // Sets Y velocity to counter gravitational force
            _yVelocity = Mathf.Sqrt(_jumpHeight * -3 * _gravity);
        }
    }
}

