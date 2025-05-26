using UnityEngine;

namespace Unknown.Samuele
{
    public class Pipes : Pausable
    {
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;
        [SerializeField] private float speed = 6f;

        private readonly float leftEdge = -11;

        [HideInInspector]
        public bool CanMove = true;

        private void Update()
        {
            if (!CanMove)
                return;
            
            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x < leftEdge)
            {
                Debug.Log("Destroying pipe");
                GetComponentInParent<PipeSpawner>().RemovePipe(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
