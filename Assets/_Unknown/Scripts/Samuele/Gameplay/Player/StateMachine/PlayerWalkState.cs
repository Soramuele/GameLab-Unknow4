using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PlayerWalkState : State<Player.PlayerStates>
    {
        public PlayerWalkState(Player.PlayerStates key, StateManager<Player.PlayerStates> context)
            : base(key, context) {  }

        private Player Player => (Player)Context;

        private float stimuliPercentage;
        private float playerSpeed;
        private float slowdownMultiplier = 1;

        public override void Enter()
        {
            stimuliPercentage = StimuliManager.Instance.Percentage;
            playerSpeed = Player.Speed;
        }

        public override void Update()
        {
            var movement = new Vector3(Player.PlayerMovement.x, 0, Player.PlayerMovement.y);
            movement = Player.Cam.transform.forward * movement.z + Player.Cam.transform.right * movement.x;
            movement.y = -1f;

            // Slowdown the player based on the amount of stimuli
            slowdownMultiplier = 1 - Mathf.Max(0f, (stimuliPercentage - Player.StimuliThreshold) / (1 - Player.StimuliThreshold)) * (1f - Player.SlowdownMultiplier);

            Player.Controller.Move(playerSpeed * slowdownMultiplier * Time.deltaTime * movement);
        }

        public override void Exit()
        {   }

        public override Player.PlayerStates GetNextState()
        {
            if (Player.PlayerMovement == Vector2.zero)
                return Player.PlayerStates.Idle;
            else if (Player.IsRunning)
                return Player.PlayerStates.Run;

            return StateKey;
        }
    }
}
