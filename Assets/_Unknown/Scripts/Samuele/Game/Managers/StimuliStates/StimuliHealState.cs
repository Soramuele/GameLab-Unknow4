using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StimuliHealState : State<StimuliManager.States>
    {
        public StimuliHealState(StimuliManager.States key, StateManager<StimuliManager.States> context)
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

        public override StimuliManager.States GetNextState()
        {
            if (StimuliManager.Percentage == 0f)
                return StimuliManager.States.Normal;
            else if (StimuliManager.RequestDamage)
                return StimuliManager.States.Damage;

            return StateKey;
        }

        private void Heal()
        {
            float healingAmount;
            
            if (StimuliManager.CurrentStimuli - StimuliManager.HealAmount > 0f)
                healingAmount = StimuliManager.CurrentStimuli - StimuliManager.HealAmount;
            else
                healingAmount = StimuliManager.CurrentStimuli;

            foreach (var source in StimuliManager.DamageSources)
                source.ReduceTotalStimuliDealt(healingAmount / StimuliManager.DamageSources.Count);

            StimuliManager.CurrentStimuli = healingAmount;

            StimuliManager.OnStimuliChangedEvent?.Invoke(StimuliManager.Percentage);
        }
    }
}
