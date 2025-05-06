using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class StudentWindowInteraction : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField] private LayerMask interactionLayer;

        private StudentMovement student;
        private Vector3 startPosition;

        public static UnityAction CloseWindowEvent;

        // Start is called before the first frame update
        void Start()
        {
            student = GetComponent<StudentMovement>();
            startPosition = transform.position;
        }

        public void GoCloseWindow(WindowInteractable targetWindow)
        {
            Debug.Log("Ok I'm going");

            student.SetDestination(targetWindow.transform.position);

            StartCoroutine(CheckForWindow(targetWindow));
        }

        private void CloseWindow()
        {
            // Close the window
            CloseWindowEvent?.Invoke();

            // Go back to starting position
            student.SetDestination(startPosition);
        }

        private IEnumerator CheckForWindow(WindowInteractable targetWindow)
        {
            Ray ray = new Ray(transform.position, targetWindow.transform.position);

            var isAtWinwow = false;
            var maxRayDistance = 2.5f;

            while (!isAtWinwow)
            {
                if (Physics.Raycast(ray, out var hit, maxRayDistance, interactionLayer))
                {
                    isAtWinwow = true;

                    hit.collider.GetComponent<WindowInteractable>().CloseWindow();
                }

                yield return null;
            }

            CloseWindow();
        }
    }
}
