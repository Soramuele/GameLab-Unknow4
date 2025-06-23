using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StimuliHealState : State<StimuliManager.StimuliState>
    {
        public StimuliHealState(StimuliManager.StimuliState key, StateManager<StimuliManager.StimuliState> context)
            : base(key, context) { }

        private StimuliManager StimuliManager => (StimuliManager)Context;

        private readonly float tick = 0.1f;
        private float timer = 0f;

        public override void Enter()
        {
            Heal();
        }

        public override void Update()
        {
            timer += Time.deltaTime;

            if (timer >= tick)
            {
                timer = 0f;
                Heal();
            }
        }

        public override void Exit()
        {
            timer = 0f;
        }

        public override StimuliManager.StimuliState GetNextState()
        {
            if (StimuliManager.Percentage == 0)
                return StimuliManager.StimuliState.Normal;
            else if (StimuliManager.RequestDamage)
                return StimuliManager.StimuliState.Damage;

            return StateKey;
        }

        private void Heal()
        {
            if (StimuliManager.CurrentStimuli - StimuliManager.HealAmount <= 0f)
                StimuliManager.CurrentStimuli = 0f;
            else
                StimuliManager.CurrentStimuli -= StimuliManager.HealAmount;

            StimuliManager.OnStimuliChangedEvent?.Invoke(StimuliManager.Percentage);
        }
    }
}
