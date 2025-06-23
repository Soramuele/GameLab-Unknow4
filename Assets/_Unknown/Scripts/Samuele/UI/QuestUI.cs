using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Unknown.Samuele
{
    public class QuestUI : Pausable
    {
        [Header("Quest")]
        [SerializeField] private RectTransform questParent;
        [SerializeField] private TMP_Text questTitle;
        [SerializeField] private TMP_Text questDescription;

        [Header("Animation")]
        [SerializeField] private float slideDuration = 0.5f;

        private Vector2 hiddenPosition = new(225f, -75f);
        private Vector2 visiblePosition = new(-225f, -75f);

        private Tween animTween;

        protected override void Start()
        {
            base.Start();

            questParent.anchoredPosition = hiddenPosition;
        }

        void OnEnable()
        {
            QuestManager.Instance.OnStartQuestEvent += StartQuest;
            QuestManager.Instance.OnCompleteQuestEvent += CloseQuest;
        }

        void OnDisable()
        {
            QuestManager.Instance.OnStartQuestEvent -= StartQuest;
            QuestManager.Instance.OnCompleteQuestEvent -= CloseQuest;
        }

        private void StartQuest(string title, string desc, float amount)
        {
            if (animTween != null && animTween.IsActive() && animTween.IsPlaying())
            {
                animTween.OnComplete(() => StartQuest(title, desc, amount));
                return;
            }
            questTitle.text = title;
            questDescription.text = amount == 0 ? desc : $"{desc} (0/{amount})";

            animTween = questParent.DOAnchorPos(visiblePosition, slideDuration).SetEase(Ease.OutExpo);
        }

        private void CloseQuest()
        {
            animTween = questParent.DOAnchorPos(hiddenPosition, slideDuration).SetEase(Ease.InExpo);
        }

        protected override void PostPause()
        {
            if (animTween != null && animTween.IsActive())
                animTween.Pause();
        }

        protected override void PostResume()
        {
            if (animTween != null && animTween.IsActive() && !animTween.IsComplete())
                animTween.Play();
        }
    }
}
