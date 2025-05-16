// using UnityEngine;
// using UnityEngine.AI;

// namespace Unknown.Samuele
// {
//     public class NPCWalking : MonoBehaviour
//     {
//         [Header("Point To Reach")]
//         [SerializeField] private Vector3 doorTransform = new Vector3(-14.4513893f, 1, -25.8563309f);
//         [SerializeField] private Vector3 outsideTransform = new Vector3(-14.5f, 1, -50.5f);
//         [SerializeField] private float checkDistance = 1;

//         [HideInInspector]
//         public bool isAtDoor = false;

//         [HideInInspector]
//         public NavMeshAgent agent;

//         private Animator animator;

//         void Awake()
//         {
//             agent = GetComponent<NavMeshAgent>();
//             animator = GetComponentInChildren<Animator>();
//         }

//         public void Start()
//         {
//             GetComponent<StudentMovement>().SetDestination(doorTransform);
//         }

//         void OnTriggerEnter(Collider other)
//         {
//             if (other.CompareTag("RevolvingDoor"))
//             {
//                 if (isAtDoor)
//                     GetComponent<StudentMovement>().SetDestination(outsideTransform);
                
//                 if (!isAtDoor)
//                 {
//                     isAtDoor = true;
//                     // Get closest opening
//                     // Set destination to that opening
//                 }
//             }
//         }
//     }
// }
