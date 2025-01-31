using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    public IState currentState { get; private set; }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        
        currentState = newState;

        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Tick()
    {
        currentState?.Tick();
    }

    public void FixedTick()
    {
        currentState?.FixedTick();
    }
}
