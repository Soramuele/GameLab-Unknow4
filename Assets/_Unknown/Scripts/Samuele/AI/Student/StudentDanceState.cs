using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StudentDanceState : State<Student.StudentStates>
    {
        public StudentDanceState(Student.StudentStates key, StateManager<Student.StudentStates> context)
            : base(key, context) {  }

        private Student Student => (Student)Context;

        private AnimatorStateInfo info;

        public override void Enter()
        {
            Student.Animator.SetBool(AnimHash.Dance, true);
            info = Student.Animator.GetCurrentAnimatorStateInfo(0);
        }

        public override void Update()
        {   }

        public override void Exit()
        {
            Student.Animator.SetBool(AnimHash.Dance, false);
        }

        public override Student.StudentStates GetNextState()
        {
            if (info.normalizedTime >= 1f)
                return Student.StudentStates.Idle;
            
            return StateKey;
        }
    }
}
