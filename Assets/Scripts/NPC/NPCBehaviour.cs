using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    [Header("NPC Walking")]
    [SerializeField, Range(0, 50)] private float walkRange = 30;
    [SerializeField] private float lifeTime = 30;
    [SerializeField] private float range = 10f;

    private NavMeshAgent agent;

    // Time
    private float startingTime;
    private float timePassed;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        startingTime = GameManager.Instance.InGameTimer;
        timePassed = startingTime;

        WalkAround(false);
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        // Timer before destroying the npc
        if (timePassed - startingTime >= lifeTime)
        {
            timePassed = startingTime;
            Destroy(gameObject);
            return;
        }

        // Patrolling();
        WalkAround();
    }

    private void WalkAround(bool isWalk = true)
    {
        if (!isWalk)
            SetDestination();

        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
            SetDestination();
    }

    private void SetDestination()
    {
        if (RandomPoint(transform.position, range, out Vector3 point)) //pass in our centre point and radius of area
                agent.SetDestination(point);
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
