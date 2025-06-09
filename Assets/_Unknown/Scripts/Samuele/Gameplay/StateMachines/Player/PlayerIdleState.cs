using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PlayerIdleState : State<Player.PlayerStates>
    {
        public PlayerIdleState(Player.PlayerStates key, StateManager<Player.PlayerStates> context)
            : base(key, context) {  }

        private Player Player => (Player)Context;

        private readonly Vector3 gravity = new Vector3(0, -1f, 0);

        public override void Enter()
        {   }

        public override void Update()
        {
            Player.Controller.Move(gravity * Time.deltaTime);
        }

        public override void Exit()
        {   }

        public override Player.PlayerStates GetNextState()
        {
            if (Player.IsJumpPressed)
                return Player.PlayerStates.Jump;
            else if (Player.PlayerMovement != Vector2.zero)
                return Player.PlayerStates.Walk;
            return StateKey;
        }
    }
}
