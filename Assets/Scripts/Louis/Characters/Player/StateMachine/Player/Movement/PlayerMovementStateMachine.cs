using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerMovementManager MovementManager { get; }

    public PlayerReusableStateData ReusableData { get; }

    public PlayerIdleState IdleState { get; }

    public PlayerRunningState RunningState { get; }

    public PlayerSlowState SlowState { get; }

    public PlayerMovementStateMachine(PlayerMovementManager playerStateMachineManager)
    {
        MovementManager = playerStateMachineManager;
        ReusableData = playerStateMachineManager.ReusableData;

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        SlowState = new PlayerSlowState(this);
    }
}
