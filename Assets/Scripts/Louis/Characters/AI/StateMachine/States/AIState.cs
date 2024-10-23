using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : IState
{
    protected AIStateMachine stateMachine;

    public AIState(AIStateMachine aIStateMachine)
    {
        stateMachine = aIStateMachine;
    }

    public virtual void Enter() { }

    public virtual void Tick() { }

    public virtual void FixedTick() { }

    public virtual void HandleInput() { }

    public virtual void Exit() { }
}
