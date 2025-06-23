using Cinemachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Camera")]
        [SerializeField] private float sensitivity = 10f;
        [SerializeField] private bool invertHorizontal;
        [SerializeField] private bool invertVertical;

        private readonly float clampAngle = 90f;
        private Vector2 deltaInput;
        private Vector3 startingRotation;

        protected override void OnEnable()
        {
            base.OnEnable();

            inputHandler.OnLookEvent += ctx => deltaInput = ctx;
        }

        void OnDisable()
        {
            inputHandler.OnLookEvent -= ctx => deltaInput = ctx;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    if (startingRotation == null)
                        startingRotation = transform.localRotation.eulerAngles;

                    startingRotation.x += deltaInput.x * sensitivity * Time.deltaTime;
                    startingRotation.y += deltaInput.y * sensitivity * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                    state.RawOrientation = Quaternion.Euler(startingRotation.y * (invertVertical ? 1 : -1),
                                                            startingRotation.x * (invertHorizontal ? -1 : 1),
                                                            0f);
                }
            }
        }
    }
}
