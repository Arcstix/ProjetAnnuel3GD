using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerStateMachineManager PlayerStateMachine { get; }

    public PlayerIdleState IdleState { get; }

    public PlayerRunningState RunningState { get; }

    public PlayerSlowState SlowState { get; }

    public PlayerMovementStateMachine(PlayerStateMachineManager playerStateMachineManager)
    {
        PlayerStateMachine = playerStateMachineManager;

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        SlowState = new PlayerSlowState(this);
    }
}