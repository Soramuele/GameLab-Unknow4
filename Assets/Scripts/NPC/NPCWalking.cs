using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : MonoBehaviour
{
    [Header("Point To Reach")]
    [SerializeField] private Vector3 doorTransform = new Vector3(-14.4513893f, 1, -25.8563309f);
    public Vector3 finalDestination = new Vector3(-14.5f, 1, -50.5f);
    [SerializeField] private float checkDistance = 1;

    public Vector3 FinalDestination => finalDestination;

    [HideInInspector]
    public NavMeshAgent agent;

    private Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Start()
    {
        agent.SetDestination(doorTransform);
        animator.SetBool("Walk", true);
    }
}
