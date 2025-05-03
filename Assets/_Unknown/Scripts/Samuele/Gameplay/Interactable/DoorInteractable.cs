using UnityEngine;

namespace Unknown.Samuele
{
    public class DoorInteractable : Interactable
    {
        [Header("Door")]
        [SerializeField] private bool needsKey;
        [SerializeField] private SOKey key;
        
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected override void Interaction()
        {
            // Make the interaction work lol
        }
    }
}
