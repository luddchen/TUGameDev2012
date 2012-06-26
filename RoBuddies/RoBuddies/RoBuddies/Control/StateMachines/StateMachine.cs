using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{ 

    class StateMachine : PlayerControl
    {
        public const String WALK_STATE = "WalkingState";
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String PULL_STATE = "PullingState";
        public const String CLIMBING_STATE = "ClimbingState";
        public const String SHOOTING_STATE = "ShootingState";
        public const String PIPE_CLIMBING_STATE = "PipeClimbingState";

        /// <summary>
        /// the current state
        /// </summary>
        public State CurrentState;

        /// <summary>
        /// list of all possible states
        /// </summary>
        public List<State> AllStates;

        /// <summary>
        /// body of this statemachine
        /// </summary>
        public IBody Body;


        public StateMachine(IBody body, Game game)
        {
            this.Body = body;
            this.Game = (RoBuddies)game;
            this.AllStates = new List<State>();
        }

        /// <summary>
        /// switch to an state by reference
        /// </summary>
        /// <param name="state">the new state</param>
        public void SwitchToState(State state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = state;
            state.Enter();
        }

        /// <summary>
        /// switch to an state by name
        /// if that state doesnt exist, state will not be switched
        /// </summary>
        /// <param name="stateName">name of new state</param>
        public void SwitchToState(String stateName)
        {
            foreach (State state in AllStates)
            {
                if (state.Name == stateName)
                {
                    if (CurrentState != null)
                    {
                        CurrentState.Exit();
                    }
                    CurrentState = state;
                    CurrentState.Enter();
                    break;
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

    }
}
