using Cinemachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class ComputerInteractable : Interactable
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("Computer Camera")]
        [SerializeField] private CinemachineVirtualCamera cam;

        [Header("Minigame")]
        [SerializeField] private SceneReference minigameCanvas;

        private CameraManager cameraManager;

        void Start()
        {
            cameraManager = CameraManager.Instance;
            cameraManager.AddCamera(cam);
        }

        protected override void Interaction()
        {
            cameraManager.SwitchCamera(cam, true);

            // Switch input map
            inputs.SetMinigame();
        }
    }
}
