using UnityEngine;
using UnityEngine.InputSystem;
using FSMOverview.Scripts.InputActions;
using FSMOverview.Scripts.ControllerFSM.States;

namespace FSMOverview.Scripts.ControllerFSM
{
    public class PlayerControllerFSM : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        private BaseState _currentState;

        public readonly IdlingState idlingState = new IdlingState();
        public readonly MovingState movingState = new MovingState();
        public readonly JumpingState jumpingState = new JumpingState();
        
        [SerializeField]
        private CharacterController _controller;
        public CharacterController Controller { get { return _controller; } }

        private Vector2 _moveInputContext;
        private float _horizontalInput;
        // Property to access horizontal input
        public float HorizontalInput { get { return _horizontalInput; } }

        private bool _jumpInput;
        // Property to access jump input
        public bool JumpInput { get { return _jumpInput; } }

        [SerializeField]
        private float _speed = 5f;
        // Property to access speed
        public float Speed { get { return _speed; } }
        
        [SerializeField]
        private float _jumpHeight = 2f;
        // Property to access jump height
        public float JumpHeight { get { return _jumpHeight; } }

        // Variables made public to access from various states, but hidden in the Inspector
        [HideInInspector]
        public Vector3 _direction,
            _velocity;

        [HideInInspector]
        public float _gravity = -9.81f,
            _yVelocity = 0f;

        // Start is called before the first frame update
        void Start()
        {
            // Sets the idling state as the default state
            TransitionToState(idlingState);
        }

        // Update is called once per frame
        void Update()
        {
            // Calls the current state's Update() method
            _currentState.Update();
        }

        public void TransitionToState(BaseState state)
        {
            // Checks if there is an active current state
            // If so, calls the current state's ExitState() method
            if (_currentState != null)
            {
                _currentState.ExitState();
            }

            // Sets current state to the state passed into the method
            _currentState = state;

            // Checks if the player controller has been assigned
            // If not, assigns this controller as player
            if (_currentState.Player == null)
            {
                _currentState.AssignPlayer(this);
            }

            // Calls the newly assigned current state's EnterState() method
            _currentState.EnterState();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // Reads value from input as Vector2
            _moveInputContext = context.ReadValue<Vector2>();

            // Sets float value for horizontal movement
            _horizontalInput = _moveInputContext.x;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // Reads value from input as bool to determine if the action was performed
            _jumpInput = context.ReadValue<float>() == 1;
        }
    }
}

