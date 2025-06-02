using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class Computer : Interactable
    {
        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera viewportCam;

        private GameManager gameManager;
        private CameraManager cameraManager;

        void Start()
        {
            gameManager = GameManager.Instance;
            cameraManager = CameraManager.Instance;
            
            cameraManager.AddCamera(viewportCam);
        }

        protected override void Interaction()
        {
            cameraManager.SwitchCamera(viewportCam);

            gameManager.ChangeInputMap(GameManager.InputMap.None);

            StartCoroutine(WaitForBlend());
        }

        private IEnumerator WaitForBlend()
        {
            yield return null;

            gameManager.HideUI();

            while (cameraManager.IsBlending)
                yield return null;

            gameManager.ChangeInputMap(GameManager.InputMap.Minigame);
        }
    }
}
