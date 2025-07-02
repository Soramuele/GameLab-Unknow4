using StateMachine;

namespace Unknown.Samuele
{
    public class StimuliNormalState : State<StimuliManager.States>
    {
        public StimuliNormalState(StimuliManager.States key, StateManager<StimuliManager.States> context)
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

        public override StimuliManager.States GetNextState()
        {
            if (StimuliManager.RequestDamage)
                return StimuliManager.States.Damage;
            
            return StateKey;
        }
    }
}
