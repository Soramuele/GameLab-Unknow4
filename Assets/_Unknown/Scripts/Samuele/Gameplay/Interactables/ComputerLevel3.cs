using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class ComputerLevel3 : Interactable
    {
        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera viewportCam;

        [Header("Minigame")]
        [SerializeField] private GameObject minigameCanvas;

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
            Debug.Log("DIo cane");
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

            minigameCanvas.SetActive(true);
        }
    }
}
