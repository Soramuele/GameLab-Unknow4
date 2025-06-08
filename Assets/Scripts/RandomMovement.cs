using UnityEngine;
using UnityEngine.AI;

public class RandomWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private float timer;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavMeshLocation(wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // Fallback if a valid point is not found
        return transform.position;
    }
}