using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


namespace Unknown.Samuele
{
    public class MenuEventSystemHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<Selectable> selectables = new();
        // [SerializeField] protected Selectable _firstSelected;

        [Header("Animations")]
        [SerializeField] protected float selectedAnimationScale = 1.1f;
        [SerializeField] protected float scaleDuration = 0.25f;

        protected Dictionary<Selectable, Vector3> scales = new();

        // protected Selectable _lastSelected;

        protected Tween scaleUpTween;
        protected Tween scaleDownTween;

        public virtual void Awake()
        {
            foreach (var _selectable in selectables)
            {
                AddSelectionListeners(_selectable);
                scales.Add(_selectable, _selectable.transform.localScale);
            }
        }

        public virtual void OnEnable()
        {
            // Ensure all selectables are reset back to original size
            for (int i = 0; i < selectables.Count; i++)
                selectables[i].transform.localScale = scales[selectables[i]];
        }

        public virtual void OnDisable()
        {
            scaleUpTween.Kill(true);
            scaleDownTween.Kill(true);
        }

        protected virtual void AddSelectionListeners(Selectable selectable)
        {
            // Add listener
            EventTrigger trigger = selectable.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = selectable.gameObject.AddComponent<EventTrigger>();

            // Add SELECT event
            EventTrigger.Entry SelectEntry = new() {
                eventID = EventTriggerType.Select
            };
            SelectEntry.callback.AddListener(OnSelect);
            trigger.triggers.Add(SelectEntry);

            // Add DESELECT event
            EventTrigger.Entry DeselectEntry = new() {
                eventID = EventTriggerType.Deselect
            };
            SelectEntry.callback.AddListener(OnDeselect);
            trigger.triggers.Add(DeselectEntry);

            // Add ONPOINTERENTER event
            EventTrigger.Entry pointerEntry = new() {
                eventID = EventTriggerType.PointerEnter
            };
            pointerEntry.callback.AddListener(OnPointerEnter);
            trigger.triggers.Add(pointerEntry);

            // Add ONPOINTEXIT event
            EventTrigger.Entry pointerExit = new() {
                eventID = EventTriggerType.PointerExit
            };
            pointerExit.callback.AddListener(OnPointerExit);
            trigger.triggers.Add(pointerExit);
        }

        public void OnSelect(BaseEventData eventData)
        {
            Vector3 newScale = eventData.selectedObject.transform.localScale * selectedAnimationScale;
            scaleUpTween = eventData.selectedObject.transform.DOScale(newScale, scaleDuration);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            Selectable sel = eventData.selectedObject.GetComponent<Selectable>();
            scaleDownTween = eventData.selectedObject.transform.DOScale(scales[sel], scaleDuration);
        }

        public void OnPointerEnter(BaseEventData eventData)
        {
            PointerEventData pointerEventData = eventData as PointerEventData;
            if (pointerEventData != null)
            {
                Selectable sel = pointerEventData.pointerEnter.GetComponentInParent<Selectable>();
                if (sel == null)
                    sel = pointerEventData.pointerEnter.GetComponentInChildren<Selectable>();
                
                pointerEventData.selectedObject = sel.gameObject;
            }
        }

        public void OnPointerExit(BaseEventData eventData)
        {
            PointerEventData pointerEventData = eventData as PointerEventData;
            if (pointerEventData != null)
                pointerEventData.selectedObject = null;
        }
    }
}
