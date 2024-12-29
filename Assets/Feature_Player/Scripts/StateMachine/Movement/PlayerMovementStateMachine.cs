using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerMovementManager MovementManager { get; }

    public PlayerReusableStateData ReusableData { get; }

    public PlayerIdleState IdleState { get; }

    public PlayerRunningState RunningState { get; }

    public PlayerWalkState SlowState { get; }

    public PlayerFallingState FallingState { get; }

    public PlayerLandingState LandingState { get; }

    public PlayerMovementStateMachine(PlayerMovementManager playerStateMachineManager)
    {
        MovementManager = playerStateMachineManager;
        ReusableData = playerStateMachineManager.ReusableData;

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        SlowState = new PlayerWalkState(this);
        FallingState = new PlayerFallingState(this);
        LandingState = new PlayerLandingState(this);
    }
}
