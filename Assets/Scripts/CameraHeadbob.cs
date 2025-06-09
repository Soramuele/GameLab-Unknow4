using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHeadBob : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Unknown.Samuele.Inputs.InputHandler inputManager;

    public float movingFrequency = 4f; // Frequency when moving
    public float movingAmplitude = 0.1f; // Amplitude when moving
    public float idleFrequency = 0.5f;   // Frequency when idle
    public float idleAmplitude = 0.2f;   // Amplitude when idle
    public float transitionSpeed = 5f;   // How fast it transitions

    private CinemachineBasicMultiChannelPerlin noise;
    private float currentFrequency;
    private float currentAmplitude;

    private Vector2 movement;

    void Start()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Start with idle values
        currentFrequency = idleFrequency;
        currentAmplitude = idleAmplitude;
    }

    void OnEnable()
    {
        inputManager.OnMovementEvent += ctx => movement = ctx;
    }
    
    void OnDisable()
    {
        inputManager.OnMovementEvent -= ctx => movement = ctx;
    }

    void Update()
    {
        bool isMoving = movement.magnitude > 0f;

        // Target values based on movement
        float targetFrequency = isMoving ? movingFrequency : idleFrequency;
        float targetAmplitude = isMoving ? movingAmplitude : idleAmplitude;

        // Smooth transition using Lerp
        currentFrequency = Mathf.Lerp(currentFrequency, targetFrequency, Time.deltaTime * transitionSpeed);
        currentAmplitude = Mathf.Lerp(currentAmplitude, targetAmplitude, Time.deltaTime * transitionSpeed);

        // Apply values to Cinemachine Noise
        noise.m_FrequencyGain = currentFrequency;
        noise.m_AmplitudeGain = currentAmplitude;
    }
}
