using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

namespace Unknown.Samuele
{
    public class CameraManager : MonoBehaviour, IManager
    {
        public static CameraManager Instance { get; private set; }

        [Header("Main Camera")]
        [SerializeField] private CinemachineVirtualCamera playerCamera;

        private CinemachineBrain mainCamera;
        private CinemachineVirtualCamera activeCamera;
        private List<CinemachineVirtualCamera> cameras;

        public UnityAction BlendingCompleteEvent;

        void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main.GetComponent<CinemachineBrain>();

            AddCamera(playerCamera);
            SwitchCamera(playerCamera);
        }

        public void AddCamera(CinemachineVirtualCamera camera)
        {
            if (!cameras.Contains(camera))
                cameras.Add(camera);
        }

        public void RemoveCamera(CinemachineVirtualCamera camera)
        {
            if (cameras.Contains(camera))
                cameras.Remove(camera);
        }

        public void SwitchCamera(CinemachineVirtualCamera camera, bool isMinigame = false)
        {
            activeCamera = camera;

            activeCamera.Priority = 20;

            foreach(var _cam in cameras)
                if (_cam != activeCamera)
                    _cam.Priority = 10;

            if (isMinigame)
                StartCoroutine(WaitForBlend());
        }

        public void SwitchToMainCamera() =>
            SwitchCamera(playerCamera);

        private IEnumerator WaitForBlend()
        {
            while (mainCamera.IsBlending)
                yield return null;
            
            yield return null;

            // Load Minigame scene
            BlendingCompleteEvent?.Invoke();
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
