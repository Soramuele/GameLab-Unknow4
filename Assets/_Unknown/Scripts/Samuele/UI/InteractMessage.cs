using UnityEngine;
using TMPro;

namespace Unknown.Samuele
{
    public class InteractMessage : MonoBehaviour
    {
        [Header("Interaction Message")]
        [SerializeField] private GameObject parent;
        [SerializeField] private TextMeshProUGUI prompt;

        void Start() =>
            parent.SetActive(false);

        void OnEnable() =>
            PlayerInteraction.SendPromptEvent += UpdateText;

        void OnDisable() =>
            PlayerInteraction.SendPromptEvent -= UpdateText;

        private void UpdateText(string message)
        {
            // Enable or disable the interaction prompt
            if (message == string.Empty)
                parent.SetActive(false);
            else
                parent.SetActive(true);
            
            // Change controls key image based on current device
            var key = "";
            var controls = GameManager.Instance.GetGameplay().currentControlScheme;
            switch (controls)
            {
                case GameplayManager.ActiveDevice.Keyboard:
                    key = "e";
                break;
                case GameplayManager.ActiveDevice.Controller:
                    key = "xx";
                break;
            }

            prompt.text = $"Press <sprite name={key}> to {message}";
        }
    }
}
