using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Unknown.Samuele
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        [Header("Main Camera")]
        [SerializeField] private CinemachineVirtualCamera playerCamera;

        private CinemachineBrain cameraBrain;
        private CinemachineVirtualCamera activeCamera;
        private List<CinemachineVirtualCamera> cameras;

        void Awake()
        {
            Instance = this;

            cameras = new List<CinemachineVirtualCamera>();
        }

        // Start is called before the first frame update
        void Start()
        {
            cameraBrain = Camera.main.GetComponent<CinemachineBrain>();

            AddCamera(playerCamera);
            SwitchToMainCamera();
        }

        public void AddCamera(CinemachineVirtualCamera camera)
        {
            cameras.Add(camera);
        }

        public void RemoveCamera(CinemachineVirtualCamera camera)
        {
            cameras.Remove(camera);
        }

        public void SwitchCamera(CinemachineVirtualCamera camera)
        {
            if (!cameras.Contains(camera))
                AddCamera(camera);
            
            activeCamera = camera;

            activeCamera.Priority = 20;

            foreach(var _cam in cameras)
                if (_cam != activeCamera)
                    _cam.Priority = 10;
        }

        public void SwitchToMainCamera() =>
            SwitchCamera(playerCamera);

        public bool IsBlending =>
            cameraBrain.IsBlending;
    }
}