using TMPro;
using UnityEngine;

namespace Unknown.Samuele
{
    public class TimeUI : Pausable
    {
        [Header("Inputs")]
        [SerializeField] private Inputs.InputHandler inputHandler;

        [Header("Time")]
        [SerializeField] private float startingTime = 450f;
        [SerializeField, Range(0.01f, 1f)] private float timerMultiplier = 1f;

        [Header("Time Text")]
        [SerializeField] private TMP_Text time;

        private bool timerHasStarted = false;
        private float timer = 0f;

        private bool isQuestTime = false;
        private float questTime = 0f;

        private QuestManager questManager;

        protected override void Start()
        {
            base.Start();

            timer = startingTime;

            CalculateTime();
        }

        void OnEnable()
        {
            questManager = QuestManager.Instance;

            inputHandler.OnMovementEvent += _ => StartTimer();
            questManager.OnTimeQuestEvent += StartQuest;
            questManager.OnTimeQuestCompleteEvent += CompleteQuest;
        }

        void OnDisable()
        {
            questManager.OnTimeQuestEvent -= StartQuest;
            questManager.OnTimeQuestCompleteEvent -= CompleteQuest;
        }

        // Update is called once per frame
        void Update()
        {
            if (!timerHasStarted)
                return;

            timer += Time.deltaTime * timerMultiplier;

            CalculateTime();

            if (isQuestTime)
            {
                if (timer >= questTime)
                    Lose();
            }
        }

        private void StartTimer()
        {
            timerHasStarted = true;

            inputHandler.OnMovementEvent -= _ => StartTimer();
        }

        private void CalculateTime()
        {
            var hours = Mathf.FloorToInt(timer / 60);
            var minutes = Mathf.FloorToInt(timer % 60);

            time.text = string.Format("{0:00}:{1:00}", hours, minutes);
        }

        private void StartQuest(float time)
        {
            isQuestTime = true;
            questTime = time;
        }

        private void CompleteQuest()
        {
            isQuestTime = false;
        }

        private void Lose()
        {
            // Lose the game
        }
    }
}
