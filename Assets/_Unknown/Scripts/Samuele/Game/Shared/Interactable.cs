using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Prompt")]
        [Tooltip("This will be the text that will show on the HUD when you will be able to interact with it")]
        [SerializeField] private string prompt;
        [Tooltip("(Alt) This will be the text that will show on the HUD when you will be able to interact with it")]
        [SerializeField] private string promptAlt;

        public string Prompt { get => prompt; }

        public void Interact()
        {
            Interaction();
        }

        protected abstract void Interaction();
    }
}
