using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StimuliDamageState : State<StimuliManager.States>
    {
        public StimuliDamageState(StimuliManager.States key, StateManager<StimuliManager.States> context)
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

        public override StimuliManager.States GetNextState()
        {
            if (timer >= healAfterDamage)
                return StimuliManager.States.Heal;
            else if (StimuliManager.Percentage == 1)
                return StimuliManager.States.Overstimuli;
            
            return StateKey;
        }

        private void ApplyDamage()
        {
            StimuliManager.CurrentStimuli = Mathf.Min(StimuliManager.CurrentStimuli + StimuliManager.DamageRequested, StimuliManager.MaxStimuli);

            StimuliManager.OnStimuliChangedEvent?.Invoke(StimuliManager.Percentage);
        }
    }
}
