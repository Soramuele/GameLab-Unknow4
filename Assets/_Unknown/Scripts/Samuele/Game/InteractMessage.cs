using UnityEngine;
using TMPro;

namespace Unknown.Samuele
{
    public class InteractMessage : MonoBehaviour
    {
        [Header("Textbox")]
        [SerializeField] private TextMeshProUGUI text;

        void OnEnable()
        {
            PlayerInteraction.SendPromptEvent += UpdateText;
        }

        void OnDisable()
        {
            PlayerInteraction.SendPromptEvent -= UpdateText;
        }

        private void UpdateText(string message)
        {
            text.text = message;
        }
    }
}
