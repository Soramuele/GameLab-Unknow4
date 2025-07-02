using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class StudentWalkState : State<Student.States>
    {
        public StudentWalkState(Student.States key, StateManager<Student.States> context)
            : base(key, context) {  }

        private Student Student => (Student)Context;

        public override void Enter()
        {
            Student.Animator.SetBool(AnimHash.Walk, true);

            Student.Agent.SetDestination(Student.Destination);
            Student.CurrentDestination = Student.Destination;
        }

        public override void Update()
        {
            if (Student.CurrentDestination != Student.Destination)
            {
                Student.Agent.SetDestination(Student.Destination);
                Student.CurrentDestination = Student.Destination;
            }
        }

        public override void Exit()
        {
            Student.Animator.SetBool(AnimHash.Walk, false);

            Student.Destination = Vector3.zero;
        }

        public override Student.States GetNextState()
        {
            if (Student.Agent.remainingDistance <= Student.RemainingDistance && !Student.BotherPlayer)
                return Student.States.Idle;
            else if (Student.Agent.remainingDistance <= Student.RemainingDistance && Student.BotherPlayer)
                return Student.States.Bother;

            return StateKey;
        }
    }
}
