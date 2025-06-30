using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class StudentIdleState : State<Student.StudentStates>
    {
        public StudentIdleState(Student.StudentStates key, StateManager<Student.StudentStates> context)
            : base(key, context) {  }

        private Student Student => (Student)Context;

        public override void Enter()
        {
            Student.Agent.isStopped = true;
        }

        public override void Update()
        {   }

        public override void Exit()
        {
            Student.Agent.isStopped = false;
        }

        public override Student.StudentStates GetNextState()
        {
            if (Student.Destination != Vector3.zero)
                return Student.StudentStates.Walk;
            else if (Student.Dancing)
                return Student.StudentStates.Dance;
            
            return StateKey;
        }
    }
}
