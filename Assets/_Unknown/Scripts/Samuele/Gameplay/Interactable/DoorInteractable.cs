using UnityEngine;

namespace Unknown.Samuele
{
    public class DoorInteractable : Interactable
    {
        [Header("Door")]
        [SerializeField] private bool needsKey;
        [SerializeField] private Item key;
        
        private Animator animator;
        private bool isOpen = false;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected override void Interaction()
        {
            if (!needsKey)
            {
                ChangeDoorState();

                return;
            }
            
            if (PlayerInventory.Instance.CheckForItem(key))
                ChangeDoorState();
            else
                Debug.Log("You need a key");
        }

        public void ChangeDoorState()
        {
            isOpen = !isOpen;
            animator.SetBool(AnimHash.DoorOpen, isOpen);

            SwitchPrompt();
        }

        private void SwitchPrompt()
        {
            if (Prompt == prompt)
                Prompt = promptAlt;
            else
                Prompt = prompt;
        }
    }
}
