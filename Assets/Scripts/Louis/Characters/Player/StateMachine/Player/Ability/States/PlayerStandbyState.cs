using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandbyState : PlayerAbilityState
{
    public PlayerStandbyState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Stanby");
        if (!metricsManager.CurrentPlayerSO.AbilityData.TransportationData.AutomaticTransportation)
        {
            AddInputAbility();
        }
        AddCancelInput();
        reusableData.CanUseAbility = true;
    }

    public override void Exit()
    {
        base.Exit();
        if (!metricsManager.CurrentPlayerSO.AbilityData.TransportationData.AutomaticTransportation)
        {
            RemoveInputShoot();
        } 
        RemoveCancelInput();
    }
}
