using UnityEngine;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class CrosshairManager : MonoBehaviour
    {
        [Header("Crosshair")]
        [SerializeField] private Image crosshair;
        [SerializeField] private Sprite interactionCrosshair;
        
        private Sprite defaultCrosshair;

        void Start() =>
            defaultCrosshair = crosshair.sprite;

        void OnEnable() =>
            PlayerInteraction.SendPromptEvent += ChangeCrosshair;
        
        void OnDisable() =>
            PlayerInteraction.SendPromptEvent -= ChangeCrosshair;

        void Update()
        {
            if (GameManager.Instance.GetGameplay().IsMinigameOn && crosshair.gameObject.activeSelf)
                crosshair.gameObject.SetActive(false);
            else if (!GameManager.Instance.GetGameplay().IsMinigameOn && !crosshair.gameObject.activeSelf)
                crosshair.gameObject.SetActive(true);
        }

        private void ChangeCrosshair(string prompt)
        {
            if (prompt != string.Empty)
            {
                // Change crosshair image when interacting with something
                crosshair.sprite = interactionCrosshair;
            }
            else
            {
                // Change crosshair image to default
                crosshair.sprite = defaultCrosshair;
            }
        }
    }
}
