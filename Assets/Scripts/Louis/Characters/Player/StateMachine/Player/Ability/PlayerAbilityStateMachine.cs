using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityStateMachine : StateMachine
{
    public PlayerStateMachineManager StateMachine { get; }

    public PlayerShootState ShootState { get; }

    public PlayerStandbyState StandbyState { get; }

    public PlayerTransportationState TransportationState { get; }

    public PlayerCancelAbilityState CancelAbilityState { get; }

    public PlayerAbilityStateMachine(PlayerStateMachineManager playerStateMachine)
    {
        StateMachine = playerStateMachine;

        ShootState = new PlayerShootState(this);
        StandbyState = new PlayerStandbyState(this);
        TransportationState = new PlayerTransportationState(this);
        CancelAbilityState = new PlayerCancelAbilityState(this);
    }
}
