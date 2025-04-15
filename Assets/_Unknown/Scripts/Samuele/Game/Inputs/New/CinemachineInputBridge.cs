using UnityEngine;
using Cinemachine;

namespace Unknown.Samuele
{
    public class CinemachineInputBridge : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private InputHandler inputHandler;

        private CinemachineVirtualCamera vcam;

        void Start() =>
            vcam = GetComponent<CinemachineVirtualCamera>();

        void OnEnable() =>
            CinemachineCore.GetInputAxis = CustomInputAxis;

        void OnDisable()
        {
            CinemachineCore.GetInputAxis = null;
        }

        float CustomInputAxis(string axisName)
        {
            return axisName switch
            {
                "Mouse X" => inputHandler.GetLook.x,
                "Mouse Y" => inputHandler.GetLook.y,
                _ => 0f
            };
        }
    }
}
