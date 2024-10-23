using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : AIState
{
    public DeadState(AIStateMachine aIStateMachine) : base(aIStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Dead State");
    }
}
