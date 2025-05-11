using System.Collections;
using UnityEngine;

namespace Unknown.Samuele
{
    public class WindowHandler : MonoBehaviour
    {
        [Header("Windows")]
        [SerializeField] private WindowInteractable[] windows;

        [Header("Time Range")]
        [Tooltip("Range of time in seconds for reopening the window. Put the minimum time in X and maximum time in Y")]
        [SerializeField] private Vector2 timeRange;
        [SerializeField] private float defaultTime = 5f;

        // Start is called before the first frame update
        void Start()
        {
            WindowInteractable openWindows = null;
            foreach(var window in windows)
            {
                if (window.isOpen && openWindows == null)
                    openWindows = window;
                else if (window.isOpen)
                    window.CloseWindow();
            }

            StartTimer();
        }

        void OnEnable()
        {
            StudentWindowInteraction.CloseWindowEvent += StartTimer;
        }

        void OnDisable()
        {
            StudentWindowInteraction.CloseWindowEvent -= StartTimer;
        }

        private void StartTimer()
        {
            var time = Random.Range(timeRange.x, timeRange.y) + defaultTime;

            StartCoroutine(Timer(Mathf.Abs(time)));
        }

        private IEnumerator Timer(float time)
        {
            var timePassed = 0f;

            while (timePassed < time)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }

            SendStudent();
        }

        private void SendStudent()
        {
            var students = FindObjectsOfType<StudentWindowInteraction>();

            if (students.Length < 1)
            {
                Debug.Log("No one avaible");
                return;
            }

            var choosenStudent = students[Random.Range(0, students.Length)];

            choosenStudent.GoCloseWindow(windows[Random.Range(0, windows.Length)]);
        }
    }
}
