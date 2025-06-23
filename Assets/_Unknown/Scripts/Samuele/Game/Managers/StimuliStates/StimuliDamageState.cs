using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StimuliDamageState : State<StimuliManager.StimuliState>
    {
        public StimuliDamageState(StimuliManager.StimuliState key, StateManager<StimuliManager.StimuliState> context)
            : base(key, context) { }

        private StimuliManager StimuliManager => (StimuliManager)Context;

        private float timer = 0f;
        private float healAfterDamage;

        public override void Enter()
        {
            healAfterDamage = StimuliManager.HealAfterDamage;

            ApplyDamage();
        }

        public override void Update()
        {
            timer += Time.deltaTime;

            if (StimuliManager.RequestDamage)
            {
                timer = 0f;
                StimuliManager.RequestDamage = false;
                ApplyDamage();
            }
        }

        public override void Exit()
        {
            timer = 0f;
        }

        public override StimuliManager.StimuliState GetNextState()
        {
            if (timer >= healAfterDamage)
                return StimuliManager.StimuliState.Heal;
            else if (StimuliManager.Percentage == 1)
                return StimuliManager.StimuliState.Overstimuli;
            
            return StateKey;
        }

        private void ApplyDamage()
        {
            if (StimuliManager.CurrentStimuli + StimuliManager.DamageRequested > StimuliManager.MaxStimuli)
                StimuliManager.CurrentStimuli = StimuliManager.MaxStimuli;
            else
                StimuliManager.CurrentStimuli += StimuliManager.DamageRequested;

            StimuliManager.OnStimuliChangedEvent?.Invoke(StimuliManager.Percentage);
        }
    }
}
