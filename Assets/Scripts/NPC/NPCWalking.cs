using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : MonoBehaviour
{
    [Header("Point To Reach")]
    [SerializeField] private Vector3 doorTransform = new Vector3(-14.4513893f, 1, -25.8563309f);
    [SerializeField] private Vector3 outsideTransform = new Vector3(-14.5f, 1, -50.5f);
    [SerializeField] private float checkDistance = 1;

    [HideInInspector]
    public bool isAtDoor = false;

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

    // Update is called once per frame
    void Update()
    {
        if (!isAtDoor)
        {
            if (Vector3.Distance(transform.position, doorTransform) < checkDistance)
            {
                isAtDoor = true;
                agent.SetDestination(outsideTransform);
            }
        }
        else
        {
            if (!agent.hasPath && !agent.pathPending)
            {
                gameObject.SetActive(false);
                NPCSpawner.NPCIsUnused(gameObject);
            }
        }
    }
}
