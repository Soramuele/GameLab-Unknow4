using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unknown.Samuele
{
    public class MenuHandler : MonoBehaviour
    {
        [Header("Selectables")]
        [SerializeField] private List<Selectable> selectables = new();
        [SerializeField] private Selectable firstSelected;

        [Header("Animations")]
        [SerializeField] protected float selectedAnimationScale = 1.1f;
        [SerializeField] protected float scaleDuration = 0.25f;
        [SerializeField] protected List<GameObject> animationExclusion = new();

        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputs;

        protected Dictionary<Selectable, Vector3> scales = new();

        protected Selectable lastSelected;

        protected Tween scaleUpTween;
        protected Tween scaleDownTween;

        protected AudioManager audioManager;

        protected virtual void Awake()
        {
            foreach (var _selectable in selectables)
            {
                AddSelectionListeners(_selectable);
                scales.Add(_selectable, _selectable.transform.localScale);
            }
        }

        protected virtual void Start()
        {
            audioManager = AudioManager.Instance;
        }

        protected virtual void OnEnable()
        {
            inputs.OnNavigateEvent += OnNavigate;

            for (int i = 0; i < selectables.Count; i++)
                selectables[i].transform.localScale = scales[selectables[i]];

            StartCoroutine(SelectAfterDelay());
        }

        protected virtual void OnDisable()
        {
            inputs.OnNavigateEvent -= OnNavigate;

            scaleUpTween.Kill(true);
            scaleDownTween.Kill(true);
        }

        protected virtual IEnumerator SelectAfterDelay()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
        }

        protected virtual void AddSelectionListeners(Selectable selectable)
        {
            // add listener
            if (!selectable.gameObject.TryGetComponent<EventTrigger>(out var trigger))
                trigger = selectable.gameObject.AddComponent<EventTrigger>();

            // add SELECT event
            EventTrigger.Entry selectEntry = new()
            {
                eventID = EventTriggerType.Select
            };
            selectEntry.callback.AddListener(OnSelect);
            trigger.triggers.Add(selectEntry);

            // add DESELECT event
            EventTrigger.Entry deselectEntry = new()
            {
                eventID = EventTriggerType.Deselect
            };
            deselectEntry.callback.AddListener(OnDeselect);
            trigger.triggers.Add(deselectEntry);

            // add ONPOINTERENTER event
            EventTrigger.Entry pointerEnter = new()
            {
                eventID = EventTriggerType.PointerEnter
            };
            pointerEnter.callback.AddListener(OnPointerEnter);
            trigger.triggers.Add(pointerEnter);

            // add ONPOINTEREXIT event
            EventTrigger.Entry pointerExit = new()
            {
                eventID = EventTriggerType.PointerExit
            };
            pointerExit.callback.AddListener(OnPointerExit);
            trigger.triggers.Add(pointerExit);
        }

        public void OnSelect(BaseEventData eventData)
        {
            // Play UI sound
            if (lastSelected != null)
                audioManager.PlayUISound();

            // Get last selected
            lastSelected = eventData.selectedObject.GetComponent<Selectable>();

            // If excluded from animation then don't animate
            if (animationExclusion.Contains(eventData.selectedObject))
                return;

            // Animate selection
            Vector3 newScale = eventData.selectedObject.transform.localScale * selectedAnimationScale;
            scaleUpTween = eventData.selectedObject.transform.DOScale(newScale, scaleDuration);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            // If excluded from animation then don't animate
            if (animationExclusion.Contains(eventData.selectedObject))
                return;
            
            // Animate deselection
            Selectable sel = eventData.selectedObject.GetComponent<Selectable>();
            scaleDownTween = eventData.selectedObject.transform.DOScale(scales[sel], scaleDuration);
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
            // Get pointer
            PointerEventData pointerEventData = eventData as PointerEventData;

            // Set pointer as selected
            if (pointerEventData != null)
            {
                pointerEventData.selectedObject = pointerEventData.pointerEnter;

                // Use this if want to remove raycast block from children/parent of the needed selectable
                /*
                Selectable sel = pointerEventData.pointerEnter.GetComponentInParent<Selectable>();

                if (sel == null)
                    sel = pointerEventData.pointerEnter.GetComponentInChildren<Selectable>();

                pointerEventData.selectedObject = sel.gameObject;
                */
            }
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            // Get pointer
            PointerEventData pointerEventData = eventData as PointerEventData;

            // Remove selected from pointer
            if (pointerEventData != null)
                pointerEventData.selectedObject = null;
        }

        // Event for UI Navigation
        protected virtual void OnNavigate()
        {
            if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null)
                EventSystem.current.SetSelectedGameObject(lastSelected.gameObject);
        }
    }
}
