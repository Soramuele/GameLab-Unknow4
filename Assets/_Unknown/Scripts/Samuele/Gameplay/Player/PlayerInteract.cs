using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class PlayerInteract : Pausable
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;
        
        [Header("Raycast data")]
        [SerializeField] private float distance;
        [SerializeField] private LayerMask interactionLayers;

        [Header("UI")]
        [SerializeField] private InteractMessage interactMessage;

        private Camera cam;
        private Ray ray;
        private Interactable interactable;

        public static UnityAction OnStartMinigameEvent;

        protected override void Start()
        {
            base.Start();

            cam = Camera.main;
        }

        void OnEnable()
        {
            inputHandler.OnInteractEvent += InteractWithObject;
        }

        void OnDisable()
        {
            inputHandler.OnInteractEvent -= InteractWithObject;
        }

        // Update is called once per frame
        void Update()
        {
            ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * distance);

            if (Physics.Raycast(ray, out var hitInfo, distance, interactionLayers))
            {
                if (hitInfo.collider.TryGetComponent<Interactable>(out var hit))
                {
                    if (interactable != null && interactable != hit)
                        ClearRaycast();
                    
                    interactable = hit;
                    interactable.EnableOutline();

                    // Update screen message
                    interactMessage.UpdateText(interactable.Prompt);
                }
                else
                {
                    ClearRaycast();
                }
            }
            else
            {
                ClearRaycast();
            }
        }

        private void InteractWithObject()
        {
            if (interactable == null)
                return;

            interactable.Interact();

            OnStartMinigameEvent?.Invoke();
        }

        private void ClearRaycast()
        {
            if (interactable != null)
            {
                interactable.DisableOutline();
                interactable = null;

                interactMessage.ClearText();
            }
        }
    }
}
