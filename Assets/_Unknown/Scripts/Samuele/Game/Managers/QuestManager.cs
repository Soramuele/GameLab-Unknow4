using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unknown.Samuele
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        private Quest assignedQuest;

        public UnityAction<string, string, float> OnStartQuestEvent;
        public UnityAction OnCompleteQuestEvent;
        public UnityAction<float> OnTimeQuestEvent;
        public UnityAction OnTimeQuestCompleteEvent;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void AssignQuest(Quest quest)
        {
            if (assignedQuest != null)
                return;
            
            assignedQuest = quest;
            OpenQuest();
        }

        public void CompleteQuest()
        {
            OnCompleteQuestEvent?.Invoke();
        }

        private void OpenQuest()
        {
            // Check if quest is time based
            if (assignedQuest.FailTime != -1)
                OnTimeQuestEvent?.Invoke(assignedQuest.FailTime);

            OnStartQuestEvent?.Invoke(assignedQuest.Title, assignedQuest.Description, assignedQuest.Amount);
        }
    }
}
