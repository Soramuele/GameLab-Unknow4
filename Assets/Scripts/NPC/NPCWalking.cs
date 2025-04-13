using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : MonoBehaviour
{
    [Header("Point To Reach")]
    [SerializeField] private Vector3 doorTransform = new Vector3(-14.4513893f, 1, -25.8563309f);
    [SerializeField] private Vector3 outsideTransform = new Vector3(-14.5f, 1, -50.5f);

    [HideInInspector]
    public bool isAtDoor = false;

    [HideInInspector]
    public NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(doorTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAtDoor)
        {
            if (Vector3.Distance(agent.transform.position, doorTransform) < 0.2f)
            {
                isAtDoor = true;
                agent.SetDestination(outsideTransform);
            }
        }
    }
}
