using UnityEngine;
using TMPro;

namespace Unknown.Samuele
{
    public class InteractMessage : MonoBehaviour
    {
        [Header("Interaction Message")]
        [SerializeField] private GameObject parent;
        [SerializeField] private TextMeshProUGUI prompt;

        private GameManager gameManager;

        private ActiveDevice controls = ActiveDevice.Keyboard;
        private string message;

        void Start()
        {
            parent.SetActive(false);

            gameManager = GameManager.Instance;
        }

        void OnEnable()
        {
            gameManager.ChangeDeviceEvent += UpdateInputIcon;
            PlayerInteraction.SendPromptEvent += UpdateText;
        }

        void OnDisable()
        {
            gameManager.ChangeDeviceEvent -= UpdateInputIcon;
            PlayerInteraction.SendPromptEvent -= UpdateText;
        }

        private void UpdateInputIcon(ActiveDevice ctx)
        {
            controls = ctx;
            UpdateText(message);
        }

        private void UpdateText(string message)
        {
            this.message = message;
            // Enable or disable the interaction prompt
            if (message == string.Empty)
            {
                parent.SetActive(false);
                return;
            }
            else
                parent.SetActive(true);
            
            // Change controls key image based on current device
            var key = "";
            switch (controls)
            {
                case ActiveDevice.Keyboard:
                    key = "e";
                break;
                case ActiveDevice.Controller:
                    key = "xx";
                break;
            }

            prompt.text = $"Press <sprite name={key}> to {message}";
        }
    }
}
