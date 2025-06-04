using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class StudentBotherState : State<Student.StudentStates>
    {
        public StudentBotherState(Student.StudentStates key, StateManager<Student.StudentStates> context)
            : base(key, context) {  }

        private Student Student => (Student)Context;

        public override void Enter()
        {
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
        }

        public override Student.StudentStates GetNextState()
        {
            if (!Student.BotherPlayer)
                return Student.StudentStates.Idle;
            
            return StateKey;
        }
    }
}
