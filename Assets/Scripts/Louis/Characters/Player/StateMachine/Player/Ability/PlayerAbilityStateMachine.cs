using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityStateMachine : StateMachine
{
    PlayerStateMachineManager PlayerStateMachine { get; }

    PlayerAutoAbilityState AutoAbilityState { get; }

    PlayerTargetAbilityState TargetAbilityState { get; }

    public PlayerAbilityStateMachine(PlayerStateMachineManager playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;

        AutoAbilityState = new PlayerAutoAbilityState(this);
        TargetAbilityState = new PlayerTargetAbilityState(this);
    }
}
