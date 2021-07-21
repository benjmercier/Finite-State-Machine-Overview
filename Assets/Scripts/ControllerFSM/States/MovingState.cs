using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMOverview.Scripts.ControllerFSM.States
{
    public class MovingState : BaseState
    {
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void Update()
        {
            CalculateMovement(_player.HorizontalInput);

            // Checks if there is any movement input
            // If not, player transitions into the idling state
            if (!IsPlayerMoving())
            {
                _player.TransitionToState(_player.idlingState);
            }

            if (_player.JumpInput)
            {
                _player.TransitionToState(_player.jumpingState);
            }
        }

        private void CalculateMovement(float horizontalInput)
        {
            _player._direction = Vector3.right * horizontalInput;
            _player._velocity = _player._direction * _player.Speed;

            _player._yVelocity += _player._gravity * Time.deltaTime;

            _player._velocity.y = _player._yVelocity;

            _player.Controller.Move(_player._velocity * Time.deltaTime);
        }
    }
}

