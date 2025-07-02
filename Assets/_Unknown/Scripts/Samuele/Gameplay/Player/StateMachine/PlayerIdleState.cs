using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class PlayerIdleState : State<Player.States>
    {
        public PlayerIdleState(Player.States key, StateManager<Player.States> context)
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

        public override Player.States GetNextState()
        {
            if (Player.PlayerMovement != Vector2.zero)
                return Player.States.Walk;
            
            return StateKey;
        }
    }
}
