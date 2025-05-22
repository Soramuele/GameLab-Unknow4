using UnityEngine;

namespace Unknown.Samuele
{
    public class Pipes : MonoBehaviour
    {
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;
        [SerializeField] private float speed = 6f;

        private readonly float leftEdge = -12;

        private bool isPaused = false;
        private PipeSpawner pipeSpawner;

        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
        public void Spawner(PipeSpawner spawner) => pipeSpawner = spawner;

        private void Update()
        {
            if (isPaused)
                return;

            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x < leftEdge)
            {
                Destroy(gameObject);
                pipeSpawner.RemovePipe(this);

            }
        }
    }
}
