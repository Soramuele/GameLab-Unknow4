using UnityEngine;

namespace Unknown.Samuele
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interaction Prompt")]
        [Tooltip("This will be the text that will be shown on the HUD when you will be able to interact with it. \n Example: Press to <prompt>")]
        [SerializeField] private string prompt;
        [Tooltip("Alternative text that will be shown on the HUD when you will be able to interact with it. \n Example: Press to <promptAlt>")]
        [SerializeField] private string promptAlt;

        private Outline outline;

        public string Prompt { get; private set; }

        void Awake()
        {
            Prompt = prompt;

            outline = GetComponent<Outline>();
            if (outline == null)
            {
                outline = gameObject.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.white;
                outline.OutlineWidth = 5f;
            }
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
