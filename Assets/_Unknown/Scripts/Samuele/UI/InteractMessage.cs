using UnityEngine;
using TMPro;

namespace Unknown.Samuele
{
    public class InteractMessage : MonoBehaviour
    {
        public static InteractMessage Instance { get; private set; }

        [Header("Interaction Message")]
        [SerializeField] private GameObject parent;
        [SerializeField] private TextMeshProUGUI prompt;

        private GameManager.CurrentDevice controls = GameManager.CurrentDevice.XBoxController;
        private string message;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            parent.SetActive(false);
        }

        void OnEnable()
        {
            GameManager.Instance.OnChangeDeviceEvent += UpdateInputIcon;
        }

        void OnDisable()
        {
            GameManager.Instance.OnChangeDeviceEvent -= UpdateInputIcon;
        }

        private void UpdateInputIcon(GameManager.CurrentDevice ctx)
        {
            controls = ctx;
        }

        public void UpdateText(string message)
        {
            if (!parent.activeSelf && message != string.Empty)
                parent.SetActive(true);

            this.message = message;

            // Change controls key image based on current device
            var key = "";
            switch (controls)
            {
                case GameManager.CurrentDevice.Keyboard_Mouse:
                    key = "e";
                    break;
                case GameManager.CurrentDevice.XBoxController:
                    key = "xx";
                    break;
                case GameManager.CurrentDevice.PlayStationController:
                    key = "ps";
                    break;
                default:
                    key = "e";
                    break;
            }

            prompt.text = $"Press <sprite name={key}> to {this.message}";
        }

        public void ClearText()
        {
            parent.SetActive(false);
            message = string.Empty;
        }

        public void Hide() =>
            prompt.gameObject.SetActive(false);

        public void Show() =>
            prompt.gameObject.SetActive(true);
    }
}
