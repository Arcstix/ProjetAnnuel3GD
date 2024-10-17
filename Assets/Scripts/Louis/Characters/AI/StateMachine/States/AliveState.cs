using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveState : AIState
{
    public AliveState(AIStateMachine aIStateMachine) : base(aIStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Alive State");
    }
}
