using UnityEngine;

namespace Unknown.Samuele
{
    public class DoorInteractable : Interactable
    {
        [Header("Door")]
        [SerializeField] private bool needsKey;
        [SerializeField] private SOKey key;
        [SerializeField] private bool alwaysNeedsKey;
        
        private Animator animator;
        private bool isDoorOpen = false;
        private bool doorAlreadyOpened;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected override void Interaction(SOKey keyObject = null)
        {
            if (!isDoorOpen)
            {
                // Close the door
                animator.SetBool("Open", false);

                return;
            }

            // Check if door can be opened freely after first open
            if (doorAlreadyOpened)
            {
                // Open the door
                OpenDoor();

                return;
            }

            if (needsKey)
            {
                // Check if player has the key
                if (keyObject == null || keyObject != key)
                {
                    Debug.Log("You need something here");
                    return;
                }

                OpenDoor();
            }
            else
            {
                OpenDoor();
            }
        }

        private void OpenDoor()
        {
            if (!alwaysNeedsKey)
                doorAlreadyOpened = true;
            
            animator.SetBool("Open", true);
        }
    }
}
