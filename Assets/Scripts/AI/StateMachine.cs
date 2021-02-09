using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public State currentState;   

    public void Initialize(State startState)
    {
        currentState = startState;
        startState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

}
