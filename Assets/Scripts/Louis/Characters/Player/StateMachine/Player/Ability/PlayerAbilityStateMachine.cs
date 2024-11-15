using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityStateMachine : StateMachine
{
    public PlayerAbilityManager AbilityManager { get; }

    public bool IsRight { get; }

    public PlayerReusableStateData ReusableStateData { get; }

    public PlayerReadyState ReadyState { get; }

    public PlayerShootState ShootState { get; }

    public PlayerStandbyState StandbyState { get; }

    public PlayerTransportationState TransportationState { get; }

    public PlayerRecallState RecallState { get; }

    public PlayerAttractionState AttractionState { get; }

    public PlayerReloadState ReloadState { get; }

    public PlayerAbilityStateMachine(PlayerAbilityManager playerStateMachine, bool isRight)
    {
        AbilityManager = playerStateMachine;
        IsRight = isRight;
        ReusableStateData = playerStateMachine.ReusableData;

        ReadyState = new PlayerReadyState(this);
        ShootState = new PlayerShootState(this);
        StandbyState = new PlayerStandbyState(this);
        TransportationState = new PlayerTransportationState(this);
        RecallState = new PlayerRecallState(this);
        AttractionState = new PlayerAttractionState(this);
        ReloadState = new PlayerReloadState(this);
    }
}
