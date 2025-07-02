using UnityEngine;
using StateMachine;

namespace Unknown.Samuele
{
    public class StudentIdleState : State<Student.States>
    {
        public StudentIdleState(Student.States key, StateManager<Student.States> context)
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

        public override Student.States GetNextState()
        {
            if (Student.Destination != Vector3.zero)
                return Student.States.Walk;
            else if (Student.Dancing)
                return Student.States.Dance;
            
            return StateKey;
        }
    }
}
