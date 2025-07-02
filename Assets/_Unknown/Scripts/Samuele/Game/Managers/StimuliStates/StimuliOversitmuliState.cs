using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StimuliOversitmuliState : State<StimuliManager.States>
    {
        public StimuliOversitmuliState(StimuliManager.States key, StateManager<StimuliManager.States> context)
            : base(key, context) { }

        private StimuliManager StimuliManager => (StimuliManager)Context;

        private float panicTimer = 0f;
        private float healTimer = 0f;
        private float healAfterPanic;
        private float timeBeforePanicing;

        public override void Enter()
        {
            healAfterPanic = StimuliManager.HealAfterPanic;
            timeBeforePanicing = StimuliManager.TimeBeforePanicing;
        }

        public override void Update()
        {
            panicTimer += Time.deltaTime;
            healTimer += Time.deltaTime;

            if (StimuliManager.RequestDamage)
                Damage();
        }

        public override void Exit()
        {
            panicTimer = 0f;
            healTimer = 0f;
        }

        public override StimuliManager.States GetNextState()
        {
            if (panicTimer >= timeBeforePanicing)
                return StimuliManager.States.Panicing;
            else if (healTimer >= healAfterPanic)
                return StimuliManager.States.Heal;

            return StateKey;
        }

        private void Damage()
        {
            healTimer = 0f;
            StimuliManager.RequestDamage = false;
        }
    }
}
