using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class StudentWalkState : State<Student.StudentStates>
    {
        public StudentWalkState(Student.StudentStates key, StateManager<Student.StudentStates> context)
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

        public override Student.StudentStates GetNextState()
        {
            if (Student.Destination == Vector3.zero)
                return Student.StudentStates.Idle;
            
            if (Student.Agent.remainingDistance <= Student.RemainingDistance && !Student.BotherPlayer)
                return Student.StudentStates.Idle;
            else if (Student.Agent.remainingDistance <= Student.RemainingDistance && Student.BotherPlayer)
                return Student.StudentStates.Bother;

            return StateKey;
        }
    }
}
