using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class PlayerJumpState : State<Player.PlayerStates>
    {
        public PlayerJumpState(Player.PlayerStates key, StateManager<Player.PlayerStates> context)
            : base(key, context) {  }

        private Player Player => (Player)Context;

        private readonly float jumpMovementSlowdown = 0.8f;
        private readonly float maxGravity = -25f;
        private float stimuliRatio;
        private float playerSpeed;
        private float jumpGravity;
        private float slowdownMultiplier = 1;

        public override void Enter()
        {
            Player.IsJumpPressed = false;

            stimuliRatio = StimuliManager.Instance.Ratio;
            jumpGravity = Player.JumpForce;
        }

        public override void Update()
        {
            var movement = new Vector3(Player.PlayerMovement.x, jumpGravity, Player.PlayerMovement.y);
            movement = Player.Cam.transform.forward * movement.z + Player.Cam.transform.right * movement.x;

            // Slowdown the player based on the amount of stimuli
            slowdownMultiplier = Mathf.Max(0f, (stimuliRatio - Player.StimuliThreshold) / (100 - Player.StimuliThreshold)) * (1f - Player.SlowdownMultiplier);

            // Mid-air movement
            if (Player.PlayerMovement != Vector2.zero)
            {
                if (Player.IsRunning)
                    playerSpeed = Player.Speed + Player.RunSpeedMultiplier;
                else
                    playerSpeed = Player.Speed;
            }
            else
            {
                playerSpeed = 0f;
            }
            
            Player.Controller.Move(playerSpeed * slowdownMultiplier * jumpMovementSlowdown * Time.deltaTime * movement);

            // Apply gravity
            jumpGravity += Player.Gravity;
            jumpGravity = Mathf.Clamp(jumpGravity, maxGravity, Player.JumpForce);
        }

        public override void Exit()
        {   }

        public override Player.PlayerStates GetNextState()
        {
            if (!Player.Controller.isGrounded)
                return StateKey;

            if (Player.PlayerMovement != Vector2.zero && !Player.IsRunning)
                return Player.PlayerStates.Walk;
            else if (Player.PlayerMovement != Vector2.zero && Player.IsRunning)
                return Player.PlayerStates.Run;

            return Player.PlayerStates.Idle;
        }
    }
}