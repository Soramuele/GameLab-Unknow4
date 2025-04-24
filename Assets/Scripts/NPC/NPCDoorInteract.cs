using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCDoorInteract : MonoBehaviour
{
    private NavMeshAgent agent;

    private bool canExit;
    public bool CanExit { get => canExit; set => canExit = value; }

    void Start() =>
        agent = GetComponent<NavMeshAgent>();

    public void EntertDoor(RevolvingDoorSpin revolvingDoor)
    {
        if (!canExit)
        {
            GameObject pos = null;
            var minDis = Mathf.Infinity;
            var currPos = transform.position;
            foreach (var _opening in revolvingDoor.enteringDoors)
            {
                var dist = Vector3.Distance(_opening.transform.position, currPos);
                if (dist < minDis)
                {
                    minDis = dist;
                    pos = _opening;
                }
            }

            StartCoroutine(EnterTheDoor(pos));
        }
    }

    IEnumerator EnterTheDoor(GameObject pos)
    {
        while (!canExit)
        {
            agent.SetDestination(pos.transform.position);
            
            yield return null;
        }

        agent.SetDestination(agent.GetComponent<NPCWalking>().finalDestination);
    }
}
