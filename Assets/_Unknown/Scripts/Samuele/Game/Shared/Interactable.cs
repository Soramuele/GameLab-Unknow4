using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interaction Prompt")]
        [Tooltip("This will be the text that will be shown on the HUD when you will be able to interact with it")]
        [SerializeField] private string prompt;
        [Tooltip("Alternative text that will be shown on the HUD when you will be able to interact with it")]
        [SerializeField] private string promptAlt;

        public string Prompt { get => prompt; }

        public void Interact()
        {
            Interaction();
        }

        public void Interact(SOKey keyObject)
        {
            Interaction(keyObject);
        }

        protected abstract void Interaction(SOKey keyObject = null);
    }
}
