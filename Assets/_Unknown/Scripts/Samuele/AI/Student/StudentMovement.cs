using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Unknown.Samuele
{
    public class StudentMovement : MonoBehaviour
    {
        [SerializeField] private bool isDancing;

        private Animator animator;
        private NavMeshAgent agent;
        private Coroutine followEntrance = null;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            if (isDancing)
                animator.SetBool(AnimHash.Dance1, true);
        }

        // Update is called once per frame
        void Update()
        {
            if (animator.GetBool(AnimHash.Walking) && agent.remainingDistance <= 0.1f)
                animator.SetBool(AnimHash.Walking, false);
        }

        public void SetDestination(Vector3 destination, bool isUpdate = false)
        {
            var _destination = new Vector3(destination.x, transform.position.y, destination.z);
            animator.SetBool(AnimHash.Walking, true);
            
            if (isUpdate)
            {
                if (followEntrance == null)
                    followEntrance = StartCoroutine(FollowTarget(_destination));
            }
            else
            {
                if (followEntrance != null)
                    StopCoroutine(FollowTarget(_destination));
                
                followEntrance = null;
                
                agent.SetDestination(_destination);
            }
        }

        private IEnumerator FollowTarget(Vector3 destination)
        {
            agent.SetDestination(destination);
            
            yield return null;
        }
    }
}
