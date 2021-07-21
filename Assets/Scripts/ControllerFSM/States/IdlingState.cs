using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMOverview.Scripts.ControllerFSM.States
{
    public class IdlingState : BaseState
    {
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void Update()
        {
            // Checks if the player is grounded and if there's any movement input
            // If so, player transitions into the moving state
            if (IsPlayerGrounded() && IsPlayerMoving())
            {
                _player.TransitionToState(_player.movingState);
            }

            // Checks if the player is grounded and if the jump input is true
            // If so, player transitions into the jumping state
            if (IsPlayerGrounded())
            {
                _player.TransitionToState(_player.jumpingState);
            }
        }
    }
}

