using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PlayerRunState : State<Player.PlayerStates>
    {
        public PlayerRunState(Player.PlayerStates key, StateManager<Player.PlayerStates> context)
            : base(key, context) {  }

        private Player Player => (Player)Context;

        private float stimuliRatio;
        private float playerSpeed;
        private float slowdownMultiplier = 1;

        public override void Enter()
        {
            stimuliRatio = StimuliManager.Instance.Ratio;
            playerSpeed = Player.Speed + Player.RunSpeedMultiplier;
        }

        public override void Update()
        {
            var movement = new Vector3(Player.PlayerMovement.x, 0, Player.PlayerMovement.y);
            movement = Player.Cam.transform.forward * movement.z + Player.Cam.transform.right * movement.x;
            movement.y = -1f;

            // Slowdown the player based on the amount of stimuli
            slowdownMultiplier = Mathf.Max(0f, (stimuliRatio - Player.StimuliThreshold) / (100 - Player.StimuliThreshold)) * (1f - Player.SlowdownMultiplier);

            Player.Controller.Move(playerSpeed * slowdownMultiplier * Time.deltaTime * movement);
        }

        public override void Exit()
        {   }

        public override Player.PlayerStates GetNextState()
        {
            if (Player.IsJumpPressed)
                return Player.PlayerStates.Jump;
            else if (Player.PlayerMovement == Vector2.zero)
                return Player.PlayerStates.Idle;
            else if (Player.PlayerMovement != Vector2.zero && !Player.IsRunning)
                return Player.PlayerStates.Walk;

            return StateKey;
        }
    }
}
