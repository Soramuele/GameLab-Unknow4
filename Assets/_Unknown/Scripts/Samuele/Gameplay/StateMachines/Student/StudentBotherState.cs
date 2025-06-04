using UnityEngine;
using StateMachine;
using DG.Tweening;

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

        public override void Enter()
        {
            stimuli = StimuliManager.Instance;
            timer = 0f;
            damage = 0f;
            stimuliDamage = Student.StimuliDamage;

            var clipIndex = Random.Range(0, Student.Audios.musicClips["Bother"].Count);
            var randomClip = Student.Audios.musicClips["Bother"][clipIndex];
            AudioManager.Instance.PlayAudio(randomClip, Student.Source);

            Student.Source.loop = true;
        }

        public override void Update()
        {
            // Update stimuli bar according to time passed
            if (timer > 5f)
            {
                stimuli.SubscribeDamagePercentage(Student.gameObject, damage);
                damage += Time.deltaTime;

                if (damage > stimuliDamage)
                    damage = stimuliDamage;
            }

            timer += Time.deltaTime;
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
                .OnComplete(() => Student.CanBotherAgain = true);
        }
    }
}
