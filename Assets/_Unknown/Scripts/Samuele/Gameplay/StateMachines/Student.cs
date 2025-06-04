using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Student : StateManager<Student.StudentStates>
    {
        public enum StudentStates
        {
            None,
            Idle,
            Walk,
            Dance,
            Bother
        }

        [Header("State")]
        [SerializeField] private StudentStates startingState;

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

            if (startingState == StudentStates.None)
                currentState = states[StudentStates.Idle];
            else
                currentState = states[startingState];
        }

        void OnEnable()
        {
            // Event for going to bother the player
            PlayerInteract.OnStartMinigameEvent += GoBotherThePlayer;
            FloppyManager.Instance.OnCloseEvent += () => botherPlayer = false;
        }

        void OnDisable()
        {
            // Event for goint to bother the player
            PlayerInteract.OnStartMinigameEvent -= GoBotherThePlayer;
            FloppyManager.Instance.OnCloseEvent -= () => botherPlayer = false;
        }

        protected override void InitializeStates()
        {
            states.Add(StudentStates.Idle, new StudentIdleState(StudentStates.Idle, this));
            states.Add(StudentStates.Walk, new StudentWalkState(StudentStates.Walk, this));
            states.Add(StudentStates.Dance, new StudentDanceState(StudentStates.Dance, this));
            states.Add(StudentStates.Bother, new StudentBotherState(StudentStates.Bother, this));
        }

        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
        }

        private void GoBotherThePlayer()
        {
            bool canGo = Random.value > 0.5f;

            if (canGo && canBotherAgain)
            {
                SetDestination(FindObjectOfType<PlayerMovement>().transform.position);
                botherPlayer = true;
            }
        }
    }
}
