using UnityEngine;

namespace Unknown.Samuele
{
    public class RevolvingDoorForAI : MonoBehaviour
    {
        [Header("Door params")]
        [SerializeField] private bool isExit;

        [Header("Destination")]
        [SerializeField] private Transform destination;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StudentMovement>(out var _student))
            {
                if (!isExit)
                    _student.SetDestination(GetComponentInParent<RevolvingDoorSpin>().GetEntrance().position, true);
                else
                    _student.SetDestination(destination.position);
            }
        }
    }
}
