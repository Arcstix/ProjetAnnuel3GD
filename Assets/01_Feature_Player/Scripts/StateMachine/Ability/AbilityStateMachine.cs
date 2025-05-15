using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStateMachine : StateMachine
{
    public PlayerAbilityManager AbilityManager { get; }
    public PlayerReusableStateData ReusableStateData { get; }
    public AbilityIdleState IdleState { get; }
    public AbilityAimState AimState { get; }
    public AbilityShootState ShootState { get; }
    public AbilityRecallState RecallState { get; }
    public AbilityRecallBothObject RecallAllState { get; }
    public AbilityThrowState ThrowState { get; }
    public AbilityMovePlayer MovePlayer { get; }
    public AbilityMoveBothObject MoveBothObject { get; }
    public AbilityMoveLeftObject MoveLeftObject { get; }
    public AbilityMoveRightObject MoveRightObject { get; }
    
    
    public AbilityStateMachine(PlayerAbilityManager playerStateMachine)
    {
        AbilityManager = playerStateMachine;
        ReusableStateData = playerStateMachine.ReusableData;
        IdleState = new AbilityIdleState(this);
        AimState = new AbilityAimState(this);
        ShootState = new AbilityShootState(this);
        RecallState = new AbilityRecallState(this);
        RecallAllState = new AbilityRecallBothObject(this);
        ThrowState = new AbilityThrowState(this);
        MovePlayer = new AbilityMovePlayer(this);
        MoveBothObject = new AbilityMoveBothObject(this);
        MoveRightObject = new AbilityMoveRightObject(this);
        MoveLeftObject = new AbilityMoveLeftObject(this);
    }
}
