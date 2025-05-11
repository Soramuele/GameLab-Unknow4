using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interaction Prompt")]
        [Tooltip("This will be the text that will be shown on the HUD when you will be able to interact with it. \n Example: Press to <prompt>")]
        [SerializeField] protected string prompt;
        [Tooltip("Alternative text that will be shown on the HUD when you will be able to interact with it. \n Example: Press to <promptAlt>")]
        [SerializeField] protected string promptAlt;

        private Outline outline;

        public string Prompt { get; protected set; }

        void Awake()
        {
            Prompt = prompt;

            outline = GetComponent<Outline>();
            outline.enabled = false;
        }

        public void EnableOutline() =>
            outline.enabled = true;

        public void DisableOutline() =>
            outline.enabled = false;

        public void Interact() =>
            Interaction();

        protected abstract void Interaction();
    }
}
