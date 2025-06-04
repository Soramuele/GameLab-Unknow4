using System;
using System.Collections.Generic;
using Unknown.Samuele;

namespace StateMachine
{
    public abstract class StateManager<TState> : Pausable where TState : Enum
    {
        protected Dictionary<TState, State<TState>> states = new Dictionary<TState, State<TState>>();
        protected State<TState> currentState;

        private bool isTransitioning = false;

        protected override void Start()
        {
            base.Start();
            
            currentState.Enter();
        }

        void Update()
        {
            TState nextStateKey = currentState.GetNextState();

            if (isTransitioning)
                return;

            if (nextStateKey.Equals(currentState.StateKey))
                currentState.Update();
            else
                TransitionToState(nextStateKey);
        }

        private void TransitionToState(TState stateKey)
        {
            UnityEngine.Debug.Log($"Transitioning to {stateKey}");
            isTransitioning = true;

            currentState.Exit();
            currentState = states[stateKey];
            currentState.Enter();

            isTransitioning = false;
        }

        protected abstract void InitializeStates();
    }
}