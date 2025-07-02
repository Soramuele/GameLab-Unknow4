using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class StudentBotherState : State<Student.States>
    {
        public StudentBotherState(Student.States key, StateManager<Student.States> context)
            : base(key, context) { }

        private Student Student => (Student)Context;

        private AudioManager audioManager;
        private DamageSource damageSource;
        private AudioClip randomClip;

        public override void Enter()
        {
            audioManager = AudioManager.Instance;
            damageSource = Student.GetComponent<DamageSource>();

            var clipIndex = Random.Range(0, Student.Audios.MusicClips["Bother"].Count);
            randomClip = Student.Audios.SFXClips["Bother"][clipIndex];
            audioManager.PlayAudio(randomClip, Student.Source);
        }

        public override void Update()
        {
            damageSource.StimulatePlayer(Time.deltaTime);
        }

        public override void Exit()
        {
            audioManager.StopAudio(Student.Source);
        }

        public override Student.States GetNextState()
        {
            if (!Student.BotherPlayer)
                return Student.States.Idle;

            return StateKey;
        }
    }
}
