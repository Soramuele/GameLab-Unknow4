using StateMachine;

namespace Unknown.Samuele
{
    public class StimuliNormalState : State<StimuliManager.StimuliState>
    {
        public StimuliNormalState(StimuliManager.StimuliState key, StateManager<StimuliManager.StimuliState> context)
            : base(key, context) { }

        private StimuliManager StimuliManager => (StimuliManager)Context;

        public override void Enter()
        { }

        public override void Update()
        { }

        public override void Exit()
        {
            StimuliManager.RequestDamage = false;
        }

        public override StimuliManager.StimuliState GetNextState()
        {
            if (StimuliManager.RequestDamage)
                return StimuliManager.StimuliState.Damage;
            
            return StateKey;
        }
    }
}
