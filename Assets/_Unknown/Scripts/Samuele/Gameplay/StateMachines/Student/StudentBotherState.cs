using UnityEngine;
using StateMachine;
using DG.Tweening;
using static System.TimeZoneInfo;

namespace Unknown.Samuele
{
    public class StudentBotherState : State<Student.StudentStates>
    {
        public StudentBotherState(Student.StudentStates key, StateManager<Student.StudentStates> context)
            : base(key, context) { }

        private Student Student => (Student)Context;

        private StimuliManager stimuli;
        private float timer;
        private float damage;
        private float stimuliDamage;
        private float somethingnew = 0;
        public GameObject player;
        public override void Enter()
        {
            stimuli = StimuliManager.Instance;
            timer = 0f;
            damage = 0f;
            stimuliDamage = Student.StimuliDamage;
            player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
            var clipIndex = Random.Range(0, Student.Audios.musicClips["Bother"].Count);
            var randomClip = Student.Audios.musicClips["Bother"][clipIndex];
            AudioManager.Instance.PlayAudio(randomClip, Student.Source);

            Student.Source.loop = true;
        }

        public override void Update()
        {
            // Update stimuli bar according to time passed
         
           
            
        }

        public override void Exit()
        {
            Student.Source.loop = false;
            RemoveStimuliFromThis();
        }

        public override Student.StudentStates GetNextState()
        {
            if (!Student.BotherPlayer)
                return Student.StudentStates.Idle;

            return StateKey;
        }

        private void RemoveStimuliFromThis()
        {
            DOTween.To(() => damage, x => damage = x, 0f, 2.5f)
                .SetUpdate(true)
                //.OnUpdate(() => Dicrese())
                .OnComplete(() => Student.CanBotherAgain = true);




        }

       
    }
}
