using System;

namespace StateMachine
{
    public abstract class State<TState> where TState : Enum
    {
        public State(TState key, StateManager<TState> context)
        {
            StateKey = key;
            Context = context;
        }

        public TState StateKey { get; private set; }
        protected StateManager<TState> Context { get; private set; }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract TState GetNextState();
    }
}
