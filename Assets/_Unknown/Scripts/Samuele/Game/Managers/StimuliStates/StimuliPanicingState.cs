using StateMachine;

namespace Unknown.Samuele
{
    public class StimuliPanicingState : State<StimuliManager.StimuliState>
    {
        public StimuliPanicingState(StimuliManager.StimuliState key, StateManager<StimuliManager.StimuliState> context)
            : base(key, context) { }

        private StimuliManager StimuliManager => (StimuliManager)Context;

        public override void Enter()
        { }

        public override void Update()
        { }

        public override void Exit()
        { }

        public override StimuliManager.StimuliState GetNextState()
        {
            return StateKey;
        }
    }
}
