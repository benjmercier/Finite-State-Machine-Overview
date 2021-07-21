using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMOverview.Scripts.ControllerFSM.States
{
    public abstract class BaseState
    {
        // Stored reference to the player controller
        protected PlayerControllerFSM _player;
        // Property to check if player has been assigned
        public PlayerControllerFSM Player { get { return _player; } }

        // Called when entering each state
        public abstract void EnterState();

        // Called when exiting each state
        public abstract void ExitState();

        // Called in player's Update()
        public abstract void Update();

        // Assigns player reference at the first state transition
        public virtual void AssignPlayer(PlayerControllerFSM playerController)
        {
            _player = playerController;
        }

        // Returns true or false based on if the player controller is grounded
        protected virtual bool IsPlayerGrounded()
        {
            return _player.Controller.isGrounded;
        }

        // Returns true or false based on if the player is moving
        protected virtual bool IsPlayerMoving()
        {            
            return Mathf.Abs(_player.HorizontalInput) > 0f;
        }
    }
}

