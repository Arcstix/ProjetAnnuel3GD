using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityStateMachine : StateMachine
{
    public PlayerAbilityManager StateMachine { get; }

    public PlayerReusableStateData ReusableStateData { get; }

    public PlayerReadyAbilityState ReadyAbilityState { get; }

    public PlayerShootState ShootState { get; }

    public PlayerStandbyState StandbyState { get; }

    public PlayerTransportationState TransportationState { get; }

    public PlayerCancelAbilityState CancelAbilityState { get; }

    public PlayerReloadAbilityState ReloadAbilityState { get; }

    public PlayerAbilityStateMachine(PlayerAbilityManager playerStateMachine)
    {
        StateMachine = playerStateMachine;
        ReusableStateData = playerStateMachine.ReusableData;

        ReadyAbilityState = new PlayerReadyAbilityState(this);
        ShootState = new PlayerShootState(this);
        StandbyState = new PlayerStandbyState(this);
        TransportationState = new PlayerTransportationState(this);
        CancelAbilityState = new PlayerCancelAbilityState(this);
        ReloadAbilityState = new PlayerReloadAbilityState(this);
    }
}
