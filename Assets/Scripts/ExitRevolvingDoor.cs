using UnityEngine;

public class ExitRevolvingDoor : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Student"))
        {
            var student = other.GetComponent<NPCDoorInteract>();
            student.CanExit = true;
        }
    }
}
