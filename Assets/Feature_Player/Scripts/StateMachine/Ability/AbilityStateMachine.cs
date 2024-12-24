using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStateMachine : StateMachine
{
    public PlayerAbilityManager AbilityManager { get; }
    public PlayerReusableStateData ReusableStateData { get; }
    public AbilityIdleState IdleState { get; }
    public AbilityTransportState TransportState { get; }
    public AbilityAimState AimState { get; }
    public AbilityShootState ShootState { get; }
    public AbilityRecallState RecallState { get; }
    
    public AbilityStateMachine(PlayerAbilityManager playerStateMachine)
    {
        AbilityManager = playerStateMachine;
        ReusableStateData = playerStateMachine.ReusableData;
        IdleState = new AbilityIdleState(this);
        TransportState = new AbilityTransportState(this);
        AimState = new AbilityAimState(this);
        ShootState = new AbilityShootState(this);
        RecallState = new AbilityRecallState(this);
    }
}
