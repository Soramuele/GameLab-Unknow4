using StateMachine;

namespace Unknown.Samuele
{
    public class StimuliPanicingState : State<StimuliManager.States>
    {
        public StimuliPanicingState(StimuliManager.States key, StateManager<StimuliManager.States> context)
            : base(key, context) { }

        private StimuliManager StimuliManager => (StimuliManager)Context;

        public override void Enter()
        { }

        public override void Update()
        { }

        public override void Exit()
        { }

        public override StimuliManager.States GetNextState()
        {
            return StateKey;
        }
    }
}
