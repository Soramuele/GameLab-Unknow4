using System;
using UnityEngine;

namespace Unknown.Samuele
{
    [RequireComponent(typeof(PlayerControls))]
    public class PlayerInteraction : PausableMonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField, Range(1, 10)] private float distance = 5f;
        [SerializeField] private LayerMask interactLayer;

        private InputHandler interactionInput;
        private Transform cameraTransform;

        private Ray ray;
        private Interactable interactable;
        private bool isInteracting = false;

        public static event Action<string> SendPromptEvent;
        private bool eventAlreadySent = false;

        protected override void Awake()
        {
            base.Awake();
            
            cameraTransform = Camera.main.transform;
            
            interactionInput = GetComponent<PlayerControls>().playerInputs;    
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
                    interactable = hit;
                    isInteracting = true;

                    if (!eventAlreadySent)
                    {
                        eventAlreadySent = true;
                        SendPromptEvent?.Invoke(hit.Prompt != string.Empty ? hit.Prompt : "Can Interact");
                    }
                }
                else
                    Debug.LogError("What?");
            }
            else
            {
                interactable = null;
                isInteracting = false;

                if (eventAlreadySent)
                {
                    eventAlreadySent = false;
                    SendPromptEvent?.Invoke(string.Empty);
                }
            }
        }

        private void InteractEvent()
        {
            if (!isInteracting)
                return;

            // Interact with object
            interactable.Interact();
        }
    }
}
