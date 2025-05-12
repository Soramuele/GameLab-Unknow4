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

        private GameManager gameManager;

       /// private ActiveDevice controls = ActiveDevice.Keyboard;
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
            gameManager = GameManager.Instance;
            //gameManager.ChangeDeviceEvent += UpdateInputIcon;
            //PlayerInteraction.SendPromptEvent += UpdateText;
        }

        void OnDisable()
        {
            //.ChangeDeviceEvent -= UpdateInputIcon;
            //PlayerInteraction.SendPromptEvent -= UpdateText;
        }

        void Update()
        {
            //if (GameManager.Instance.GetGameplay().IsMinigameOn && parent.activeSelf)
              //  parent.SetActive(false);
        }

        //private void UpdateInputIcon(ActiveDevice ctx)
       // {
//controls = ctx;
       //     UpdateText(message);
       // }

        public void UpdateText(string message)
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
            var key = "e";
            //switch (controls)
           // {
           //     case ActiveDevice.Keyboard:
           //         key = "e";
//break;
           //     case ActiveDevice.Controller:
           //         key = "xx";
           //     break;
            //    default:
           //         key = "e";
            //    break;
            //}

            prompt.text = $"Press <sprite name={key}> to {message}";
        }
    }
}
