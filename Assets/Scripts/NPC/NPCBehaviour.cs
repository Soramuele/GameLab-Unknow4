using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    [Header("NPC data")]
    [SerializeField, Range(0, 50)] private float walkRange = 30;
    [SerializeField] private float lifeTime = 30;
    public float range = 10f;

    private NavMeshAgent agent;

    // Time
    private float startingTime;
    private float timePassed;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        timePassed = startingTime = GameManager.Instance.InGameTimer;
    }

    void Start()
    {
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

    // private bool isWalkSet;
    // private Vector3 walkPoint;

    // private void Patrolling()
    // {
    //     if (!isWalkSet)
    //         SearchWalkPoint();

    //     if (isWalkSet)
    //         agent.SetDestination(walkPoint);

    //     // Check if destination is reached
    //     Vector3 distanceToWalk = transform.position - walkPoint;
    //     if (distanceToWalk.magnitude < 1.2f)
    //         isWalkSet = false;
    // }

    // // Return random point in range
    // private void SearchWalkPoint()
    // {
    //     // Set random point in range
    //     float randomX = Random.Range(-walkRange, walkRange);
    //     float randomZ = Random.Range(-walkRange, walkRange);

    //     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //     if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
    //         isWalkSet = true;
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.transform.CompareTag("NPC"))
    //         isWalkSet = false;
    // }
}
