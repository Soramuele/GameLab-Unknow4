using StateMachine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Student : StateManager<Student.States>
    {
        public enum States
        {
            None,
            Idle,
            Walk,
            Dance,
            Bother
        }

        [Header("State")]
        [SerializeField] private States startingState;

        [Header("Movement")]
        [SerializeField] private float remainingDistance = 0.1f;
        private Vector3 destination = Vector3.zero;
        private Vector3 currentDestination;

        [Header("Audio")]
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private SOAudio audios;

        [Header("Stimuli")]
        [SerializeField] private float stimuliDamage;

        private NavMeshAgent agent;
        private Animator anim;

        private bool dancing = false;
        private bool botherPlayer = false;
        private bool canBotherAgain = true;

        // Getters
        public NavMeshAgent Agent => agent;
        public Animator Animator => anim;
        public float RemainingDistance => remainingDistance;
        public Vector3 Destination { get => destination; set => destination = value; }
        public Vector3 CurrentDestination { get => currentDestination; set => currentDestination = value; }
        public AudioSource Source => source;
        public AudioSource SFXSource => sfxSource;
        public SOAudio Audios => audios;
        public float StimuliDamage => stimuliDamage;
        public bool Dancing => dancing;
        public bool BotherPlayer => botherPlayer;
        public bool CanBotherAgain { get => canBotherAgain; set => canBotherAgain = value; }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();

            InitializeStates();

            if (startingState == States.None)
                currentState = states[States.Idle];
            else
                currentState = states[startingState];
        }

        void OnEnable()
        {
            // Event for going to bother the player
            PlayerInteract.OnStartMinigameEvent += () => StartCoroutine(GoBotherThePlayer());
            FloppyManager.Instance.OnCloseEvent += () => botherPlayer = false;
        }

        void OnDisable()
        {
            // Event for goint to bother the player
            PlayerInteract.OnStartMinigameEvent -= () => StartCoroutine(GoBotherThePlayer());
            FloppyManager.Instance.OnCloseEvent -= () => botherPlayer = false;
        }

        protected override void InitializeStates()
        {
            states.Add(States.Idle, new StudentIdleState(States.Idle, this));
            states.Add(States.Walk, new StudentWalkState(States.Walk, this));
            states.Add(States.Dance, new StudentDanceState(States.Dance, this));
            states.Add(States.Bother, new StudentBotherState(States.Bother, this));
        }

        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
        }

        private IEnumerator GoBotherThePlayer()
        {
            yield return new WaitForSeconds(10);
            bool canGo = Random.value > 0f;

            if (canGo && canBotherAgain)
            {
                SetDestination(FindObjectOfType<Player>().transform.position);
                botherPlayer = true;
            }
        }
    }
}
