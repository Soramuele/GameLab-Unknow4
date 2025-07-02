using StateMachine;
using UnityEngine;

namespace Unknown.Samuele
{
    public class StudentDanceState : State<Student.States>
    {
        public StudentDanceState(Student.States key, StateManager<Student.States> context)
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

        public override Student.States GetNextState()
        {
            if (info.normalizedTime >= 1f)
                return Student.States.Idle;
            
            return StateKey;
        }
    }
}
