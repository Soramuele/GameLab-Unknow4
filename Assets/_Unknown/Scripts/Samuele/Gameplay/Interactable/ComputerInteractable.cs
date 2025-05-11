using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unknown.Samuele
{
    public class ComputerInteractable : Interactable
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputs;

        [Header("Computer Camera")]
        [SerializeField] private CinemachineVirtualCamera cam;

        [Header("Minigame")]
        [SerializeField] private SceneReference minigameScene;

        private CameraManager cameraManager;

        void Start()
        {
            cameraManager = CameraManager.Instance;
            cameraManager.AddCamera(cam);
        }

        protected override void Interaction()
        {
            cameraManager.SwitchCamera(cam);

            // Switch input map
            inputs.SetMinigame();

            StartCoroutine(WaitForBlend());
        }

        private IEnumerator WaitForBlend()
        {
            yield return new WaitForSeconds(.1f);

            while (cameraManager.IsBlending)
                yield return null;
            
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }
}
