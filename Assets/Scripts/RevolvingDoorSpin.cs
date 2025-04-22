using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RevolvingDoorSpin : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField, Range(0.1f, 10f)] private float spinSpeed = .5f;
    
    public float rotationSpeed = 90f;
    public Transform doorCenter;
    public float arcAngle = 90f;
    public float angularSpeed = 90f;
    public float radius = 1f;
    private Vector3 exitPoint = new Vector3(-14.77f, 2.5f, -25.3f);

    // Update is called once per frame
    private void Update()
    {
        doorCenter.Rotate(0, -angularSpeed * Time.deltaTime, 0, Space.World);
        // doorCenter.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Student"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                StartCoroutine(MoveAgentThroughDoor(agent));
            }
        }
    }

    private IEnumerator MoveAgentThroughDoor(NavMeshAgent agent)
    {
        agent.isStopped = true;

        float angle = 0f;
        float direction = 1f;
        Vector3 startOffset = (agent.transform.position - doorCenter.position).normalized * radius;

        while (angle < arcAngle)
        {
            float step = angularSpeed * Time.deltaTime;
            angle += step;

            Vector3 rotatedOffset = Quaternion.Euler(0, step * direction, 0) * startOffset;
            agent.transform.position = doorCenter.position + rotatedOffset;
            startOffset = rotatedOffset;

            agent.transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, rotatedOffset));

            yield return null;
        }

        agent.Warp(agent.transform.position); // Sync NavMeshAgent
        agent.isStopped = false;
        agent.SetDestination(exitPoint);
    }
}
