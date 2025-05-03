using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(PlayerControls))]
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField, Range(1, 10)] private float distance = 3.25f;
        [SerializeField] private LayerMask interactLayer;

        private InputHandler interactionInput;
        private Transform cameraTransform;

        private Ray ray;
        private Interactable interactable;

        public static UnityAction<string> SendPromptEvent;

        void Awake()
        {
            interactionInput = GetComponent<PlayerControls>().PlayerInputs;
            cameraTransform = Camera.main.transform;
        }

        void OnEnable() =>
            interactionInput.InteractEvent += InteractEvent;

        void OnDisable() =>
            interactionInput.InteractEvent -= InteractEvent;

        // Update is called once per frame
        void Update()
        {
            // Create a ray at the center of the camera
            ray = new Ray(cameraTransform.transform.position, cameraTransform.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance);
            
            // Check if raycast is hitting an interactable object
            if (Physics.Raycast(ray, out var hitInfo, distance, interactLayer))
            {
                if (hitInfo.collider.TryGetComponent<Interactable>(out var hit))
                {
                    if (interactable != null)
                        interactable.DisableOutline();
                    
                    interactable = hit;
                    interactable.EnableOutline();

                    SendPromptEvent?.Invoke(hit.Prompt);
                }
            }
            else
            {
                if (interactable != null)
                    interactable.DisableOutline();
                
                interactable = null;

                SendPromptEvent?.Invoke(string.Empty);
            }
        }

        private void InteractEvent()
        {
            if (interactable == null)
                return;

            // Interact with object
            interactable.Interact();
        }
    }
}
