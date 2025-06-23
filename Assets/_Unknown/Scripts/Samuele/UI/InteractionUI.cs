using TMPro;
using UnityEngine;

namespace Unknown.Samuele
{
    public class InteractionUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject parent;
        [SerializeField] private TMP_Text promptMessage;

        private string controlsKey = "e";

        private GameManager gameManager;

        void OnEnable()
        {
            gameManager = GameManager.Instance;

            gameManager.OnChangeDeviceEvent += UpdateInputIcon;
        }

        void OnDisable()
        {
            gameManager.OnChangeDeviceEvent -= UpdateInputIcon;
        }

        private void UpdateInputIcon(CurrentDevice ctx)
        {
            controlsKey = ctx switch
            {
                CurrentDevice.Keyboard_Mouse => "e",
                CurrentDevice.XBoxController => "xx",
                CurrentDevice.PlayStationController => "ps",
                _ => "e",
            };
        }

        public void ShowText(string message)
        {
            parent.SetActive(true);

            promptMessage.text = $"<sprite name={controlsKey}> to {message}";
        }

        public void HideText()
        {
            parent.SetActive(false);
        }
    }
}
