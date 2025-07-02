using UnityEngine;
using UnityEngine.Events;
using StateMachine;
using System.Collections.Generic;

namespace Unknown.Samuele
{
    public class StimuliManager : StateManager<StimuliManager.States>
    {
        public static StimuliManager Instance { get; private set; }

        public enum States
        {
            Normal,
            Heal,
            Damage,
            Overstimuli,
            Panicing
        }

        [Header("Stimuli Amount")]
        [SerializeField] private float maxStimuli = 450f;

        [Header("Heal")]
        [SerializeField] private float healAfterDamage = 0.5f;
        [SerializeField] private float healAmount = 1f;

        [Header("Overstimuli")]
        [SerializeField] private float healAfterPanic = 3.5f;
        [SerializeField] private float timeBeforePanicing = 10f;
        [SerializeField] private float panicingTime = 3f;

        private float currentStimuli = 0f;
        private bool requestDamage = false;
        private float damageRequested = 0f;
        private bool isSafeZone = false;
        private List<DamageSource> damageSources = new();

        // Getters
        public float MaxStimuli => maxStimuli;
        public float CurrentStimuli { get => currentStimuli; set => currentStimuli = value; }
        public float HealAfterDamage => healAfterDamage;
        public float HealAfterPanic => healAfterPanic;
        public float HealAmount => healAmount;
        public float TimeBeforePanicing => timeBeforePanicing;
        public float PanicingTime => panicingTime;
        public bool RequestDamage { get => requestDamage; set => requestDamage = value; }
        public float DamageRequested => damageRequested;
        public List<DamageSource> DamageSources => damageSources;
        
        public float Percentage { get => currentStimuli / maxStimuli; }

        public UnityAction<float> OnStimuliChangedEvent;

        void Awake()
        {
            Instance = this;

            InitializeStates();

            currentState = states[States.Normal];
        }

        protected override void InitializeStates()
        {
            states.Add(States.Normal, new StimuliNormalState(States.Normal, this));
            states.Add(States.Heal, new StimuliHealState(States.Heal, this));
            states.Add(States.Damage, new StimuliDamageState(States.Damage, this));
            states.Add(States.Overstimuli, new StimuliOversitmuliState(States.Overstimuli, this));
            states.Add(States.Panicing, new StimuliPanicingState(States.Panicing, this));
        }

        public void ApplyDamage(DamageSource damageSource, float value, out bool canDamage)
        {
            if (isSafeZone)
            {
                canDamage = false;
                return;
            }

            canDamage = true;
            requestDamage = true;
            damageRequested = value;

            if (!damageSources.Contains(damageSource))
                damageSources.Add(damageSource);
        }

        public void SetSafeZone(bool value) =>
            isSafeZone = value;

        public void RemoveSource(DamageSource source) =>
            damageSources.Remove(source);
    }
}
