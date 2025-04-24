using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RevolvingDoorSpin : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField, Range(0.1f, 10f)] private float spinSpeed = .5f;
    
    public float rotationSpeed = 90f;
    public float arcAngle = 359f;
    public float radius = 1f;
    public Vector3 finishPoint = new Vector3(-16.989f, 2f, -60.21f);

    public GameObject[] enteringDoors;

    [HideInInspector] public bool canExit;

    // Update is called once per frame
    private void Update()
    {
        // door.Rotate(0, -angularSpeed * Time.deltaTime, 0, Space.World);
        door.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Student"))
        {
            var student = other.GetComponent<NPCDoorInteract>();
            student.EntertDoor(this);
            
        }
    }

    // public void StopThisCoroutine()
    // {
    //     Debug.Log("ASHFOOASN");
    //     StopCoroutine(nameof(MoveAgentThroughDoor));
        
    //     agent.SetDestination(finishPoint);
    // }

    // private IEnumerator MoveAgentThroughDoor(NavMeshAgent agent)
    // {
    //     agent.enabled = false;

    //     // Get neaerst opening
    //     GameObject pos = null;
    //     var minDis = Mathf.Infinity;
    //     var currPos = agent.transform.position;
    //     foreach (var _opening in enteringDoors)
    //     {
    //         var dist = Vector3.Distance(_opening.transform.position, currPos);
    //         if (dist < minDis)
    //         {
    //             minDis = dist;
    //             pos = _opening;
    //         }
    //     }

    //     var angle = 0f;

    //     while (angle < arcAngle * 1.2f)
    //     {
    //         // Debug.Log(canExit);
    //         float step = rotationSpeed * Time.deltaTime;
    //         angle += step;

    //         // agent.SetDestination(pos.transform.position);

    //         agent.transform.position = pos.transform.position;

    //         yield return null;
    //     }

    //     agent.enabled = true;
    //     agent.SetDestination(finishPoint);
    // }
}
