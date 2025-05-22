using UnityEngine;
using Cinemachine;

namespace Unknown.Samuele
{
    public class CinemachineInputBridge : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        private Vector2 playerLook;

        private CinemachineVirtualCamera vcam;

        void Start() =>
            vcam = GetComponent<CinemachineVirtualCamera>();

        void OnEnable()
        {
            inputHandler.OnLookEvent += ctx => playerLook = ctx;
            CinemachineCore.GetInputAxis = CustomInputAxis;
        }

        void OnDisable()
        {
            inputHandler.OnLookEvent -= ctx => playerLook = ctx;
            CinemachineCore.GetInputAxis = null;
        }

        float CustomInputAxis(string axisName)
        {
            return axisName switch
            {
                "Mouse X" => playerLook.x,
                "Mouse Y" => playerLook.y,
                _ => 0f
            };
        }
    }
}