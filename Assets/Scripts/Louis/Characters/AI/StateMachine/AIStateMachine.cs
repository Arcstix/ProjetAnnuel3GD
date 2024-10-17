using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : StateMachine
{
    public AIManager Manager { get; }

    public AliveState AliveState { get; }

    public DeadState DeadState { get; }

    public AIStateMachine(AIManager aIManager)
    {
        Manager = aIManager;
        AliveState = new AliveState(this);
        DeadState = new DeadState(this);
    }
}
