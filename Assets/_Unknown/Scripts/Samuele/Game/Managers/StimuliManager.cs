using UnityEngine;
using UnityEngine.Events;
using StateMachine;

namespace Unknown.Samuele
{
    public class StimuliManager : StateManager<StimuliManager.StimuliState>
    {
        public static StimuliManager Instance { get; private set; }

        public enum StimuliState
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

        /// <summary> Stimuli percentage in decimal (0 to 1) </summary>
        public float Percentage { get => currentStimuli / maxStimuli; }

        public UnityAction<float> OnStimuliChangedEvent;

        void Awake()
        {
            Instance = this;

            InitializeStates();

            currentState = states[StimuliState.Normal];
        }

        protected override void InitializeStates()
        {
            states.Add(StimuliState.Normal, new StimuliNormalState(StimuliState.Normal, this));
            states.Add(StimuliState.Heal, new StimuliHealState(StimuliState.Heal, this));
            states.Add(StimuliState.Damage, new StimuliDamageState(StimuliState.Damage, this));
            states.Add(StimuliState.Overstimuli, new StimuliOversitmuliState(StimuliState.Overstimuli, this));
            states.Add(StimuliState.Panicing, new StimuliPanicingState(StimuliState.Panicing, this));
        }

        public void ApplyDamage(float value)
        {
            requestDamage = true;
            damageRequested = value;
        }
    }
}
