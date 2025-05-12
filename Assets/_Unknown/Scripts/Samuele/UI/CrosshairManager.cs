using UnityEngine;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class CrosshairManager : MonoBehaviour
    {
        public static CrosshairManager Instance { get; private set; }
        [Header("Crosshair")]
        [SerializeField] private Image crosshair;
        [SerializeField] private Sprite interactionCrosshair;
        
        private Sprite defaultCrosshair;

        private void Awake()
        {
            Instance = this;
        }

        void Start() =>
            defaultCrosshair = crosshair.sprite;

   

        public void ChangeCrosshair(string prompt)
        {
            if (prompt != string.Empty)
            {
                // Change crosshair image when interacting with something
                crosshair.gameObject.SetActive(false);
            }
            else
            {
                // Change crosshair image to default
                crosshair.gameObject.SetActive(true);
            }
        }
    }
}
