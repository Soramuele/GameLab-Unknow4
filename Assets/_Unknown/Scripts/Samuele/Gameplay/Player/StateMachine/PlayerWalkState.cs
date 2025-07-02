using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PlayerWalkState : State<Player.States>
    {
        public PlayerWalkState(Player.States key, StateManager<Player.States> context)
            : base(key, context) {  }

        private Player Player => (Player)Context;

        private AudioManager audioManager;
        private float stimuliPercentage;
        private float playerSpeed;
        private float slowdownMultiplier = 1;
        private float tick = 0.2f;
        private float timer;

        public override void Enter()
        {
            audioManager = AudioManager.Instance;
            stimuliPercentage = StimuliManager.Instance.Percentage;
            playerSpeed = Player.Speed;

            timer = 0f;
        }

        public override void Update()
        {
            var movement = new Vector3(Player.PlayerMovement.x, 0, Player.PlayerMovement.y);
            movement = Player.Cam.transform.forward * movement.z + Player.Cam.transform.right * movement.x;
            movement.y = -1f;

            // Slowdown the player based on the amount of stimuli
            slowdownMultiplier = 1 - Mathf.Max(0f, (stimuliPercentage - Player.StimuliThreshold) / (1 - Player.StimuliThreshold)) * (1f - Player.SlowdownMultiplier);

            Player.Controller.Move(playerSpeed * slowdownMultiplier * Time.deltaTime * movement);

            timer += Time.deltaTime;
            if (timer > tick)
            {
                timer = 0f;
                audioManager.PlayFootsteps(Player.Audios.SFXClips["Footsteps"][0], Player.SFXSource);
            }
        }

        public override void Exit()
        {   }

        public override Player.States GetNextState()
        {
            if (Player.PlayerMovement == Vector2.zero)
                return Player.States.Idle;
            else if (Player.IsRunning)
                return Player.States.Run;

            return StateKey;
        }
    }
}
